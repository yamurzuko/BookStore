using BookStore.Application.AuthorOperations.GetAuthorDetail;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.AuthorOperationTests.GetAuthorDetailTests
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]

        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(null, null);
            command.AuthorId = authorId;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(1)]

        public void WhenInvalidAuthorIdAreGiven_Validator_ShouldNotBeReturnErrors(int authorId)
        {
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(null, null);
            command.AuthorId = authorId;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

