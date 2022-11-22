using BookStore.Application.AuthorOperations.DeleteAuthor;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.AuthorOperationTests.DeleteAuthorTests
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]

        public void WhenInvalidAuthorIdAreGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = authorId;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(1)]

        public void WhenInvalidAuthorIdAreGiven_Validator_ShouldNotBeReturnErrors(int authorId)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = authorId;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
