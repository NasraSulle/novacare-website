<script>
    window.printReferralElement = function (elementId) {
        const content = document.getElementById(elementId);
    if (!content) return;

    const printWindow = window.open('', '_blank', 'width=900,height=1000');
    printWindow.document.write(`
    <html>
        <head>
            <title>Medical Referral Letter</title>
            <style>
                * {box - sizing: border-box; margin: 0; padding: 0; }
                body {
                    font - family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
                color: #1f2937;
                font-size: 13.5px;
                line-height: 1.5;
                background: #ffffff;
                        }
                .letter-container {width: 100%; display: flex; flex-direction: column; }
                .letter-header {
                    background - color: #faf082 !important;
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
                padding: 20px 56px;
                display: flex;
                justify-content: space-between;
                align-items: center;
                        }
                .brand-logo {display: flex; align-items: center; gap: 10px; color: #1c1917; }
                .brand-name {font - family: Georgia, "Times New Roman", serif; font-size: 28px; font-weight: 600; }
                .header-contact {font - family: Georgia, "Times New Roman", serif; font-size: 12px; line-height: 1.4; text-align: right; }
                .letter-body {padding: 36px 56px 40px 56px; }
                .letter-title {font - size: 28px; font-weight: 600; color: #111827; margin-bottom: 14px; }
                .section-block {margin - bottom: 16px; }
                .section-title {font - weight: 700; color: #111827; margin-bottom: 2px; }
                .re-line {font - weight: 700; color: #111827; margin-bottom: 16px; }
                .signature-block {margin - top: 24px; margin-bottom: 32px; }
                .sign-off {margin - bottom: 24px; }
                .letter-footer {margin - top: 32px; padding-top: 12px; border-top: 1px solid #e5e7eb; font-size: 11.5px; color: #6b7280; }
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
</script>
