using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.TwitchClientClasses;

namespace TwitchLib.Tests.Tests
{
    public class ChatClientTest : TestResult
    {
        public enum ChatClientTestType
        {
            Connected = 0,
            NewSubscriber = 1,
            ReSubscriber = 2,
            Message = 3,
            Command = 4,
            ViewerJoin = 5,
            ViewerLeave = 6,
            ModeratorJoin = 7,
            ModeratorLeave = 8,
            IncorrectLogin = 9,
            HostLeft = 10,
            HostStarted = 11, 
            HostStopped = 12,
            ChannelState = 13,
            UserState = 14,
            Ping = 15,
            ExistingUsers = 16
        }

        public ChatClientTestType TestType { get; protected set; }
        public string TestString { get; protected set; }
        public bool Successful { get; protected set; } = false;
        public Exception FailedException { get; protected set; }
        public string ExtraInfo { get; protected set; } = "N/A";

        public ChatClientTest(ChatClientTestType _type, string _testString, string _channel)
        {
            TestType = _type;
            TestString = _testString;
            switch(_type)
            {
                case ChatClientTestType.Connected:
                    Test_Connected(_testString, _channel);
                    break;
                case ChatClientTestType.NewSubscriber:
                    Test_NewSubscriber(_testString, _channel);
                    break;
                case ChatClientTestType.ReSubscriber:
                    Test_ReSubscriber(_testString, _channel);
                    break;
                case ChatClientTestType.Message:
                    Test_Message(_testString, _channel);
                    break;
                case ChatClientTestType.Command:
                    Test_Message(_testString, _channel);
                    break;
                case ChatClientTestType.ViewerJoin:
                    Test_ViewerJoin(_testString, _channel);
                    break;
                case ChatClientTestType.ViewerLeave:
                    Test_ViewerLeave(_testString, _channel);
                    break;
                case ChatClientTestType.ModeratorJoin:
                    Test_ModeratorJoin(_testString, _channel);
                    break;
                case ChatClientTestType.ModeratorLeave:
                    Test_ModeratorLeave(_testString, _channel);
                    break;
                case ChatClientTestType.IncorrectLogin:
                    Test_IncorrectLogin(_testString, _channel);
                    break;
                case ChatClientTestType.HostLeft:
                    Test_HostLeft(_testString, _channel);
                    break;
                case ChatClientTestType.HostStarted:
                    Test_HostStarted(_testString, _channel);
                    break;
                case ChatClientTestType.HostStopped:
                    Test_HostStopped(_testString, _channel);
                    break;
                case ChatClientTestType.ChannelState:
                    Test_ChannelState(_testString, _channel);
                    break;
                case ChatClientTestType.UserState:
                    Test_UserState(_testString, _channel);
                    break;
                case ChatClientTestType.Ping:
                    Test_Ping(_testString, _channel);
                    break;
                case ChatClientTestType.ExistingUsers:
                    Test_ExistingUsers(_testString, _channel);
                    break;
            }
        }

        private void Test_Connected(string _testString, string _channel)
        {
            try
            {
                Successful = ChatParsing.detectConnected(_testString);
            } catch(Exception ex)
            {
                FailedException = ex;
            }
        }

        private void Test_NewSubscriber(string _testString, string _channel)
        {
            try
            {
                if(ChatParsing.detectNewSubscriber(_testString, _channel))
                {
                    NewSubscriber sub = new NewSubscriber(_testString);
                    if (sub.Channel == _channel && (sub.Name != null && sub.Name.Length > 1))
                        Successful = true;
                } else
                {
                    Successful = false;
                }
            } catch(Exception ex)
            {
                FailedException = ex;
            }
        }

        private void Test_ReSubscriber(string _testString, string _channel)
        {

        }

        private void Test_Message(string _testString, string _channel)
        {

        }

        private void Test_Command(string _testString, string _channel)
        {

        }

        private void Test_ViewerJoin(string _testString, string _channel)
        {

        }

        private void Test_ViewerLeave(string _testString, string _channel)
        {

        }

        private void Test_ModeratorJoin(string _testString, string _channel)
        {

        }

        private void Test_ModeratorLeave(string _testString, string _channel)
        {

        }

        private void Test_IncorrectLogin(string _testString, string _channel)
        {

        }

        private void Test_HostLeft(string _testString, string _channel)
        {

        }

        private void Test_HostStarted(string _testString, string _channel)
        {

        }

        private void Test_HostStopped(string _testString, string _channel)
        {

        }

        private void Test_ChannelState(string _testString, string _channel)
        {

        }

        private void Test_UserState(string _testString, string _channel)
        {

        }

        private void Test_Ping(string _testString, string _channel)
        {

        }

        private void Test_ExistingUsers(string _testString, string _channel)
        {

        }
    }
}
