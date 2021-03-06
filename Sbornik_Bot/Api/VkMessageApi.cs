﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace Sbornik_Bot
{
    public class VkMessageApi: IMessageApi
    {
        private VkApi _vkApi;
        private static string server; //for vk api (is used for every query)
        private static string key; //for vk api (is used for every query)
        private string ts; //for vk api (is used for every query), changes for every query 
        private int wait;
        public VkMessageApi(string token, ulong groupId)
        {
            _vkApi = new VkApi();
            _vkApi.Authorize(new ApiAuthParams{AccessToken = token});
            var response = _vkApi.Groups.GetLongPollServer(groupId);
            server = response.Server;
            key = response.Key;
            ts = response.Ts;
            wait = 25;
        }
        public IEnumerable<Message> NewMessages()
        {
            var events_response = _vkApi.Groups.GetBotsLongPollHistory(new BotsLongPollHistoryParams()
            {
                Key = key, Server = server, Ts = ts, Wait = wait
            });
            ts = events_response.Ts;
            var messages = events_response.Updates
                .Where(u => u.Type == GroupUpdateType.MessageNew)
                .Select(u => u.MessageNew.Message);
            return messages;
        }

        public void SendMessage(MessagesSendParams sendParams)
        {
            _vkApi.Messages.Send(sendParams);
        }
        
    }
}