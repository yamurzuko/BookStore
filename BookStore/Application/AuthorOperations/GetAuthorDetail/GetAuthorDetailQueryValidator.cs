using FluentValidation;

namespace BookStore.Application.AuthorOperations.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
	{
		public GetAuthorDetailQueryValidator()
		{
            RuleFor(query => query.AuthorId).GreaterThan(0);
        }
	}
}

