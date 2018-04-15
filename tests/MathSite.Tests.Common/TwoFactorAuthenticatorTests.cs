using System;
using System.Collections.Generic;
using System.Text;
using Google.Authenticator;
using Xunit;

namespace MathSite.Tests.Common
{
    public class TwoFactorAuthenticatorTests
    {
        [Fact]
        public  void TwoFactorAuthenticatorCreated()
        {
            var tfa = new TwoFactorAuthenticator();
            Assert.NotNull(tfa);
        }
        [Fact]
        public void SetupInfoCreated()
        {
            var tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("Math site", "login", "key", 300, 300);
            Assert.NotNull(setupInfo);
        }
        [Fact]
        public void QrCodeSetupImageUrlCreated()
        {
            var tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("Math site", "login", "key", 300, 300);
            Assert.NotNull(setupInfo.QrCodeSetupImageUrl);
        }
        [Fact]
        public void ManualEntryKeyCreated()
        {
            var tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("Math site", "login", "key", 300, 300);
            Assert.NotNull(setupInfo.ManualEntryKey);
        }
        [Fact]
        public void IsValidIfTokenIsCorrect()
        {
            var key = "key";
            var tfa = new TwoFactorAuthenticator();
            var token = tfa.GetCurrentPIN(key);
            var setupInfo = tfa.GenerateSetupCode("Math site", "login", key, 300, 300);
            var isValid = tfa.ValidateTwoFactorPIN(key, token);
            Assert.True(isValid);
        }
        [Fact]
        public void IsNotValidIfTokenIsNotCorrect()
        {
            var key = "key";
            var tfa = new TwoFactorAuthenticator();
            var token = tfa.GetCurrentPIN(key);
            var setupInfo = tfa.GenerateSetupCode("Math site", "login", key, 300, 300);
            var isValid = tfa.ValidateTwoFactorPIN(key, token+key);
            Assert.False(isValid);
        }
    }
}
