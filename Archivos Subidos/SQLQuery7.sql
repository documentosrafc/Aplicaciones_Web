


SELECT B.MarkedInvoice FACTURA_PAGADA, A.*
FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTRANS A
INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.LedgerJournalTrans B ON A.VOUCHER=B.VOUCHER
AND A.TXT LIKE '%REC%'

--SELECT TransRefId,* FROM [10.0.0.54].AX50DOM_LIVE.DBO.CustTransIdRef A
--INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTRANS B ON A.RECID=B.RECID
--WHERE B.TXT LIKE '%REC%'