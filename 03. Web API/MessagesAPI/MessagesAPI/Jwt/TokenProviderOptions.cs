﻿using Microsoft.IdentityModel.Tokens;
using System;

namespace MessagesAPI.Jwt
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(15);

        public SigningCredentials SigningCredentials { get; set; }
    }
}