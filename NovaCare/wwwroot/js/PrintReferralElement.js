    window.printReferralElement = function (elementId) {
        const content = document.getElementById(elementId);
    if (!content) return;

    const printWindow = window.open('', '_blank', 'width=900,height=1000');
    printWindow.document.write(`
    <html>
        <head>
            <title>Medical Referral Letter</title>
            <style>
               .letter-container {
        max-width: 794px;
        margin: 0 auto;
        background-color: #ffffff;
        color: #1f2937;
        font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
        font-size: 13.5px;
        line-height: 1.5;
        box-shadow: 0 2px 10px rgba(0, 0, 0, 0.08);
        display: flex;
        flex-direction: column;
        min-height: 1050px;
    }

    .letter-header {
        border-bottom: 5px solid rgba(22,35,104,1);
        padding: 20px 35px;
        display: flex;
        justify-content: space-between;
        align-items: center;
    }

    .brand-logo {
        display: flex;
        align-items: center;
        gap: 10px;
        color: rgba(22,35,104,1);
    }

    .brand-name {
        font-family: Georgia, "Times New Roman", serif;
        font-size: 20px;
        font-weight: 600;
        color: rgba(22,35,104,1);
        letter-spacing: -0.5px;
    }

    .header-contact {
        font-family: Georgia, "Times New Roman", serif;
        font-size: 12px;
        line-height: 1.4;
        color: rgba(22,35,104,1);
        text-align: right;
    }

    .letter-body {
        padding: 36px 56px 40px 56px;
        flex: 1;
        display: flex;
        flex-direction: column;
    }

    .letter-title {
        font-size: 28px;
        font-weight: 600;
        color: #111827;
        letter-spacing: -0.5px;
        margin-bottom: 14px;
    }

    .section-block {
        margin-bottom: 16px;
    }

    .section-title {
        font-weight: 700;
        color: #111827;
        margin-bottom: 2px;
    }

    .re-line {
        font-weight: 700;
        color: #111827;
        margin-bottom: 16px;
        text-align: center !important;
    }

    .signature-block {
        margin-top: 24px;
        margin-bottom: 32px;
    }

    .sign-off {
        margin-bottom: 24px;
    }

    .letter-footer {
        margin-top: auto;
        padding-top: 12px;
        border-top: 1px solid #e5e7eb;
        font-size: 11.5px;
        color: #6b7280;
    }

    @@media print {
        body * {
            visibility: hidden;
        }
        #printable-referral, #printable-referral * {
            visibility: visible;
        }
        #printable-referral {
            position: absolute;
            left: 0;
            top: 0;
            width: 100%;
            box-shadow: none;
            min-height: auto;
        }
        .no-print {
            display: none !important;
        }
        .letter-header {
            -webkit-print-color-adjust: exact;
            print-color-adjust: exact;
        }
    }
            </style>
        </head>
        <body>
            ${content.outerHTML}
        </body>
    </html>
    `);

    printWindow.document.close();
    printWindow.focus();
        setTimeout(() => {
        printWindow.print();
    printWindow.close();
        }, 250);
    };

