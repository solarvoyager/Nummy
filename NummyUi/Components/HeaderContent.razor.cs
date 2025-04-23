using AntDesign;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using NummyShared.Dtos;
using NummyUi.Services;
using NummyUi.Session;

namespace NummyUi.Components
{
    public partial class HeaderContent
    {
        private UserToListDto? _user;
        private NoticeIconData[] _notifications = [];
        private NoticeIconData[] _messages = [];
        private NoticeIconData[] _events = [];
        private int _count = 0;

        private List<AutoCompleteDataItem<string>> DefaultOptions { get; set; } =
        [
            new()
            {
                Label = "umi ui",
                Value = "umi ui"
            },

            new()
            {
                Label = "Pro Table",
                Value = "Pro Table"
            },

            new()
            {
                Label = "Pro Layout",
                Value = "Pro Layout"
            }
        ];

        private AvatarMenuItem[] AvatarMenuItems { get; set; } =
        [
            //new() { Key = "center", IconType = "setting", Option = "个人中心"},
            new() { Key = "account", IconType = "user", Option = "Account"},
            //new() { IsDivider = true },
            new() { Key = "logout", IconType = "logout", Option = "Log out"}
        ];

        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IProjectService ProjectService { get; set; }
        [Inject] protected IMessageService MessageService { get; set; }
        [Inject] protected IUserSession UserSession { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            SetClassMap();

            _user = UserSession.GetUser();
            
            var notices = await ProjectService.GetNoticesAsync();
            _notifications = notices.Where(x => x.Type == "notification").Cast<NoticeIconData>().ToArray();
            _messages = notices.Where(x => x.Type == "message").Cast<NoticeIconData>().ToArray();
            _events = notices.Where(x => x.Type == "event").Cast<NoticeIconData>().ToArray();
            _count = notices.Length;
        }

        protected void SetClassMap()
        {
            ClassMapper
                .Clear()
                .Add("right");
        }

        public void HandleSelectUser(MenuItem item)
        {
            switch (item.Key)
            {
                /*case "center":
                    NavigationManager.NavigateTo("/account/center");
                    break;*/
                case "account":
                    NavigationManager.NavigateTo("/user/account");
                    break;
                case "logout":
                    NavigationManager.NavigateTo("/user/login");
                    break;
            }
        }

        public void HandleSelectLang(MenuItem item)
        {
        }

        public async Task HandleClear(string key)
        {
            switch (key)
            {
                case "notification":
                    _notifications = new NoticeIconData[] { };
                    break;
                case "message":
                    _messages = new NoticeIconData[] { };
                    break;
                case "event":
                    _events = new NoticeIconData[] { };
                    break;
            }
            await MessageService.Success($"清空了{key}");
        }

        public async Task HandleViewMore(string key)
        {
            await MessageService.Info("Click on view more");
        }
    }
}