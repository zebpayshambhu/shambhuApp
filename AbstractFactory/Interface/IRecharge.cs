using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory.Interface
{
    public enum RechargeType
    {
        UtilityRecharge = 1,
        WalletRecharge
    }

    public class UtilityRechargeModel
    {
        public long TransactionId { get; set; }
        public decimal Amount { get; set; }
        public int ServiceProviderId { get; set; }
        public string Particular { get; set; }
    }
    public class WalletRechargeModel
    {
        public long TransactionId_1 { get; set; }
        public decimal Amount { get; set; }
        public int ServiceProviderId { get; set; }
        public string Particular { get; set; }
    }

    //public class RechargeFactory
    //{
    //    public static RechargeAbstract GetInstance(RechargeType rechargeType)
    //    {
    //        switch (rechargeType)
    //        {
    //            case RechargeType.UtilityRecharge:
    //                return new UtilityRecharge();
    //            case RechargeType.WalletRecharge:
    //                return new WalletRecharge();
    //        }
    //        return null;
    //    }
    //}

    public abstract class RechargeAbstract
    {
        public abstract void InitiateRequest();
    }

    public abstract class RechargeRequestAbstract: RechargeAbstract
    {
        public void PaymentRequest(long TransactionId, decimal Amount, int ServiceProviderId, string Particular) { }

        public abstract bool IsValidateRequest();
    }

    public abstract class RechargeResponseAbstract
    {
        public void PaymentResponse() { }
    }

    public class UtilityRecharge : RechargeRequestAbstract
    {
        private UtilityRechargeModel _model = null;
        public UtilityRecharge(UtilityRechargeModel model)
        {
            _model = model;
        }
        public override bool IsValidateRequest()
        {
            return true;
        }

        public override void InitiateRequest()
        {
            if (IsValidateRequest())
            {
                PaymentRequest(_model.TransactionId, _model.Amount, _model.ServiceProviderId, _model.Particular);
            }
        }
    }

    public class WalletRecharge : RechargeRequestAbstract
    {
        private WalletRechargeModel _model = null;
        public WalletRecharge(WalletRechargeModel model)
        {
            _model = model;
        }

        public override bool IsValidateRequest()
        {
            return true;
        }

        public override void InitiateRequest()
        {
            if (IsValidateRequest())
            {
                PaymentRequest(_model.TransactionId_1, _model.Amount, _model.ServiceProviderId, _model.Particular);
            }
        }
    }
}
