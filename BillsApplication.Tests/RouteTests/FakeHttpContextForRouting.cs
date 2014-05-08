using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BillsApplication.Tests.RouteTests
{
    class FakeHttpContextForRouting : HttpContextBase
    {

        FakeHttpRequestForRouting _fRequest;
        FakeHttpResponseForRouting _fResponse;

        public FakeHttpContextForRouting(string appPath = "/", string requestUrl = "~/")
        {

            //Create a new fake Request and Response object
            _fRequest = new FakeHttpRequestForRouting(appPath, requestUrl);
            _fResponse = new FakeHttpResponseForRouting();

        }

        // This property returns the fake request
        public override HttpRequestBase Request
        {
            get { return _fRequest; }
        }

        // This property returns the fake request
        public override HttpResponseBase Response
        { 
            get { return _fResponse; }
        }

        
    }
}
