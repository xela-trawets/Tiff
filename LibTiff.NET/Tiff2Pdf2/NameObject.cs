﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BitMiracle.Docotic.PDFLib
{
    class NameObject : PDFObject
    {
        private string m_value = null;

        public NameObject(string value)
        {
            SetValue(value);
        }

        public NameObject(NameObject nameObj)
        {
            if (nameObj == null)
                throw new PdfException(PdfExceptionType.InvalidParameter);

            SetValue(nameObj.m_value);
        }

        //public NameObject& operator=(const NameObject& nameObj);

        public override PDFObject Clone()
        {
            return new NameObject(this);
        }

        public override string ToString()
        {
            return "/" + m_value;
        }

        public override PDFObject.Type GetPDFType()
        {
            return PDFObject.Type.PDFName;
        }

        public void SetValue(string value)
        {
            if (value == null || value.Length == 0)
                throw new PdfException(PdfExceptionType.InvalidParameter);

            if (value.Length > Limit.LIMIT_MAX_PDFNAME_LEN)
                throw new PdfException(PdfExceptionType.NameIsTooLong);

            PDFUtils.PdfStrCpy(ref m_value, value, Limit.LIMIT_MAX_PDFNAME_LEN);
        }

        public string GetValue()
        {
            return m_value;
        }

        public override void Write(PDFStream stream)
        {
            stream.WriteEscapeName(m_value);
        }

        protected override void clearImpl()
        {
        }
    }
}