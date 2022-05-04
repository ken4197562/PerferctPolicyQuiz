 public class ApiRequest<T> : IApiRequest<T>
        {
            private static HttpClient _client;
            // required to gain access to the context
            private readonly HttpContext _httpContext;
            public ApiRequest(IHttpContextAccessor httpContextAccessor)
            {
                // injecting a reference to the current context
                _httpContext = httpContextAccessor.HttpContext;

                if (_client == null)
                {
                    _client = new HttpClient();
                    _client.BaseAddress = new Uri("https://localhost:44361/api/");
                    _client.DefaultRequestHeaders.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                // if true, a token exists in the session
                if (_httpContext.Session.GetString("Token") != null)
                {
                    // add the token to the HttpClient 
                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _httpContext.Session.GetString("Token"));
                }

            }
