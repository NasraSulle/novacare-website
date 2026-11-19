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


window.printLabResultElement = function (elementId) {
    const originalContent = document.getElementById(elementId);
    if (!originalContent) {
        console.error("Element not found:", elementId);
        return;
    }

    // 1. Clone the element so we don't modify the live Blazor UI
    const clonedContent = originalContent.cloneNode(true);

    // 2. Physically remove all elements marked with 'no-print'
    clonedContent.querySelectorAll('.no-print').forEach(el => el.remove());

    // 3. Convert textareas (like Test Result Conclusion) into auto-expanding divs
    const originalTextareas = originalContent.querySelectorAll('textarea');
    const clonedTextareas = clonedContent.querySelectorAll('textarea');

    clonedTextareas.forEach((clonedTa, index) => {
        const textValue = originalTextareas[index]?.value || clonedTa.value || '';
        const textDiv = document.createElement('div');
        textDiv.textContent = textValue;
        textDiv.style.whiteSpace = 'pre-wrap';      // Preserves line breaks & wrapping
        textDiv.style.wordBreak = 'break-word';     // Prevents long words from overflowing
        textDiv.style.fontFamily = 'inherit';
        textDiv.style.fontSize = 'inherit';
        textDiv.style.lineHeight = '1.5';
        textDiv.style.padding = '8px 0';
        textDiv.style.minHeight = '120px';

        // Replace the textarea inside the MudBlazor input wrapper
        clonedTa.parentNode.replaceChild(textDiv, clonedTa);
    });

    // 4. Copy stylesheets from the main page
    const styles = Array.from(document.querySelectorAll('link[rel="stylesheet"], style'))
        .map(style => style.outerHTML)
        .join('\n');

    const printWindow = window.open('', '_blank', 'width=900,height=1000');
    printWindow.document.write(`
    <html>
        <head>
            <title>Lab Result</title>
            ${styles}
            <style>
                body {
                    background: white !important;
                    margin: 0;
                    padding: 16px;
                }
                #${elementId} {
                    width: 100% !important;
                    box-shadow: none !important;
                }
                /* Expand fixed-height table so all rows print */
                .mud-table-container {
                    height: auto !important;
                    max-height: none !important;
                    overflow: visible !important;
                }
                /* Ensure MudInput wrappers expand naturally */
                .mud-input-control, .mud-input, .mud-input-slot {
                    height: auto !important;
                    max-height: none !important;
                    overflow: visible !important;
                }
            </style>
        </head>
        <body>
            ${clonedContent.outerHTML}
        </body>
    </html>
    `);

    printWindow.document.close();
    printWindow.focus();

    setTimeout(() => {
        printWindow.print();
        printWindow.close();
    }, 500);
};





