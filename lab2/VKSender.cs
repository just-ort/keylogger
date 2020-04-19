using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;w

namespace lab2
{
    public static class VKSender
    {
        private static VkApi _api;
        
        static VKSender()
        {
            _api = new VkApi();

            _api.Authorize(new ApiAuthParams
            {
                // id - id of ur vk-app, Login - phone numb, passowrd - password)0
                ApplicationId = 7418993, Login = "+7", Password = "pw",
                Settings = Settings.All,
                // group token
                AccessToken = "token"
            });
        }

        public static bool SendMessage(string message)
        {
            try
            {
                _api.Messages.Send(new MessagesSendParams
                {
                    // group id
                    GroupId = 123,
                    Message = message,
                    RandomId = new Random().Next(),
                    // short name of ur page (from address browser field)
                    Domain = "ya_dura4ok",
                    // id of your vk page
                    PeerId = _api.UserId.Value
                });
                
                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}