using AntDesign;
using NummyUi.Utils;

namespace NummyUi.Pages.User.Account
{
    public partial class Index
    {
        private string _selectKey = NummyContants.AccountMenuDefaultItemKey;

        private void SelectKey(MenuItem item)
        {
            _selectKey = item.Key;
        }
    }
}