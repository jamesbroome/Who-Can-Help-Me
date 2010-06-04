﻿namespace WhoCanHelpMe.Web.Controllers.Profile.ViewModels
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Diagnostics;

    using Shared.ViewModels;

    #endregion

    [DebuggerDisplay("{FirstName} {LastName}")]
    public class ProfilePageViewModel : PageViewModel
    {
        public ProfilePageViewModel()
        {
            this.Assertions = new List<ProfileAssertionViewModel>();
        }

        public IList<ProfileAssertionViewModel> Assertions { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}