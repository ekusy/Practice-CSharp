﻿using System.Reactive.Disposables;
using System.Windows;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PracticeViewModels.Calc
{
    public class CalcViewModel : BindableBase, IDisposable
    {
        #region フィールド・プロパティ

        #region 定数
        #endregion

        private readonly CompositeDisposable disposables = [];

        #region ReactiveProperty
        /// <summary>単価</summary>
        public ReactivePropertySlim<string> UnitPrice { get; set; } = new ReactivePropertySlim<string>("1000");
        /// <summary>数量</summary>
        public ReactivePropertySlim<string> Amount { get; set; } = new ReactivePropertySlim<string>("10");
        /// <summary>割引率</summary>
        public ReactivePropertySlim<string> DiscountRate { get; set; } = new ReactivePropertySlim<string>("0");
        /// <summary>合計金額</summary>
        public ReactivePropertySlim<string> TotalPrice { get; set; } = new ReactivePropertySlim<string>("0");
        /// <summary>実行中かどうか</summary>
        public ReactivePropertySlim<bool> IsIdle { get; set; } = new ReactivePropertySlim<bool>(true);

        /// <summary>計算開始</summary>
        public AsyncReactiveCommand CommandExecute { get; set; } = new AsyncReactiveCommand();

        #endregion

        #endregion

        #region 公開メソッド
        public CalcViewModel()
        {
            _ = this.CommandExecute.Subscribe(this.OnCommandExecute).AddTo(this.disposables);
        }

        public void Dispose()
        {
            this.disposables.Clear();
        }
        #endregion

        #region コールバック

        /// <summary>計算ボタン押下時のコールバック</summary>
        private async Task OnCommandExecute()
        {
            // asyncに警告を出さないためのおまじない
            await Task.Delay(0);

            this.Calc();
        }

        #endregion

        #region 非公開メソッド

        /// <summary>計算して結果を格納する</summary>
        protected virtual void Calc()
        {
            // 入力パラメータを集める
            var unitPrice = 0;
            var amount = 0;
            var discountRate = 0.0;

            try
            {
                unitPrice = this.GetPrice();
                amount = this.GetAmount();
                discountRate = this.GetDiscountRate();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }

            // 合計金額計算ユースケースを呼び出す
            this.TotalPrice.Value = (unitPrice * amount * ((100 - discountRate) / 100.0)).ToString();
        }

        /// <summary>単価を数値として取得する</summary>
        /// <exception cref="ArgumentException"></exception>
        private int GetPrice()
        {
            if (!int.TryParse(this.UnitPrice.Value, out var value))
            {
                throw new ArgumentException("単価は0より大きい数値を設定してください");
            }

            if (value <= 0)
            {
                throw new ArgumentException("単価は0より大きい数値を設定してください");
            }

            return value;
        }



        /// <summary>個数を数値として取得する</summary>
        /// <exception cref="ArgumentException"></exception>
        private int GetAmount()
        {
            if (!int.TryParse(this.Amount.Value, out var value))
            {
                throw new ArgumentException("個数は0より大きい数値を設定してください");
            }

            if (value <= 0)
            {
                throw new ArgumentException("個数は0より大きい数値を設定してください");
            }

            return value;
        }



        /// <summary>割引率を数値として取得する</summary>
        /// <exception cref="ArgumentException"></exception>
        private double GetDiscountRate()
        {
            if (!int.TryParse(this.DiscountRate.Value, out var value))
            {
                throw new ArgumentException("割引率には0~100を入力してください");
            }

            if (value < 0 || value > 100.0)
            {
                throw new ArgumentException("割引率には0~100を入力してください");
            }

            return value;
        }

        #endregion


    }
}
