using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.Enums
{
    public enum Gender
    {
        female,
        male
    }

    public enum PaymentType
    {
        Wire_Transfer,
        Cash,
        Cheque,
        Card,
        MNO
    }

    public enum CardType
    {
        MNO,
        Bank,
        VC
    }

    public enum MobileOperator
    {
        M_PESA,
        TIGO_PESA,
        HALOPESA,
        AIRTEL_MONEY
    }

    public enum BankType
    {
        NMB,
        CRDB,
        PBZ,
        NBC,
        Stanbic_Bank,
        Equity,
        Exim,
        Access,
        Standard_Chartered,
        KCB,
        DTB,
        Barter_Trade
    }
}
