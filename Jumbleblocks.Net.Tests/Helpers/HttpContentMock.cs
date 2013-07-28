﻿using System.Net.Http;
using Moq;

namespace Tests.Jumbleblocks.Net.Helpers
{
    public class HttpContentMock
    {
        private Mock<HttpContent> _httpContent = new Mock<HttpContent>();
        public HttpContent Object { get { return _httpContent.Object; } }

        public void Set_ContentDispositionTo_FormData()
        {
            Object.Headers.Add("Content-Disposition", "form-data");
        }

        public void Set_ContentTypeTo_MultipartFormDataWithBoundary()
        {
            Object.Headers.Add("Content-Type", "multipart/form-data; boundary=---12344");
        }

        public void Set_ContentTypeTo_TextHtml()
        {
            Object.Headers.Add("Content-Type", "text/html");
        }
    }
}