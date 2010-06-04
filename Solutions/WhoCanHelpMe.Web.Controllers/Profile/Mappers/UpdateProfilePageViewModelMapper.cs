﻿namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Framework.Mapper;
    using WhoCanHelpMe.Web.Controllers.Profile.ViewModels;
    using WhoCanHelpMe.Web.Controllers.Shared.Mappers.Contracts;

    using Profile = WhoCanHelpMe.Domain.Profile;

    public class UpdateProfilePageViewModelMapper : IMapper<Profile, IList<Category>, UpdateProfilePageViewModel>
    {
        private readonly IMapper<Category, CategoryViewModel> categoryViewModelMapper;

        private readonly IPageViewModelBuilder pageViewModelBuilder;

        private readonly IMapper<Assertion, ProfileAssertionViewModel> profileAssertionViewModelMapper;

        public UpdateProfilePageViewModelMapper(
            IPageViewModelBuilder pageViewModelBuilder,
            IMapper<Assertion, ProfileAssertionViewModel> profileAssertionViewModelMapper,
            IMapper<Category, CategoryViewModel> categoryViewModelMapper)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
            this.profileAssertionViewModelMapper = profileAssertionViewModelMapper;
            this.categoryViewModelMapper = categoryViewModelMapper;
            Mapper.CreateMap<Profile, UpdateProfilePageViewModel>();
        }

        public UpdateProfilePageViewModel MapFrom(
            Profile input,
            IList<Category> categories)
        {
            var viewModel = Mapper.Map<Profile, UpdateProfilePageViewModel>(input);

            viewModel.Assertions = Extensions.MapAllUsing(input.Assertions
                    .OrderBy(a => a.Category.SortOrder.ToString("00000") + a.Tag.Name)
                    .ToList(), this.profileAssertionViewModelMapper);

            this.AddCategoriesTo(
                viewModel,
                categories);

            return this.pageViewModelBuilder.UpdateSiteProperties(viewModel);
        }

        private void AddCategoriesTo(
            UpdateProfilePageViewModel model,
            IList<Category> categories)
        {
            model.Categories = Extensions.MapAllUsing(categories.OrderBy(c => c.SortOrder).ToList(), this.categoryViewModelMapper);
        }
    }
}