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
        MNO,
        Insurance // Used when recording an insurance claim payout against a Bill
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

    public enum ClaimStatus
    {
        Not_Applicable = 0,     // Pure cash items/bills
        Draft = 1,              // Claim created in hospital system, not yet sent
        Submitted = 2,          // Sent to NHIF/ZHIF API or Smart/LCT TPA portal
        Queried = 3,            // Insurer requested additional clinical notes/documents
        Approved = 4,           // 100% of claimed amount approved
        Partially_Approved = 5, // Some items approved, some rejected
        Rejected = 6,           // Entire claim denied
        Settled = 7             // Insurer has paid the money to the hospital
    }

    public enum InsuranceIntegrationType
    {
        Direct_API = 1, // e.g., NHIF, ZHIF direct integration
        TPA_Portal = 2, // e.g., Smart Applications / LCT (Jubilee, Strategis, AAR, Britam)
        Manual = 3      // Printable claim forms / manual batch submission
    }
}
