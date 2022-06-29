using Pizza.Data.Entities;
using Pizza.Bll.Helpers;
using Pizza.Tests.Helpers;

namespace Pizza.Tests
{
    public class ProductFilteringServiceTests
    {
        private readonly IQueryable<Product> _products;

        public ProductFilteringServiceTests()
        {
            _products = TestDataProvider.GetTestDataForServiceTests();
        }

        [Fact]
        public void FilterByPrice_WhenThereAreItemsBetweenTheTwoPrices_ReturnsProductsWithApropiatePrices()
        {
            // Arrange
            decimal? minPrice = 15;
            decimal? maxPrice = 20;
            var expected = 8;

            // Act
            var filteredProducts = _products.FilterByPrice(minPrice, maxPrice);
            var actual = filteredProducts.Count();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterByPrice_WhenThereAreNoItemsBetweenTheTwoPrices_ReturnsNoProducts()
        {
            // Arrange
            decimal? minPrice = 200;
            decimal? maxPrice = 300;
            var expected = 0;

            // Act
            var filteredProducts = _products.FilterByPrice(minPrice, maxPrice);
            var actual = filteredProducts.Count();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterByPrice_WhenBothParametersAreNull_ReturnsEveryProducts()
        {
            // Arrange
            decimal? minPrice = null;
            decimal? maxPrice = null;
            var expected = _products.Count();

            // Act
            var filteredProducts = _products.FilterByPrice(minPrice, maxPrice);
            var actual = filteredProducts.Count();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SearchByTerm_WhenThereAreMatchesOnlyOnName_ReturnsProductsWhoseNameContainsTheTerm()
        {
            // Arrange
            var term = "Cheese &";
            var expected = 2;

            // Act
            var filteredProducts = _products.SearchByTerm(term);
            var actual = filteredProducts.Count();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SearchByTerm_WhenThereAreMatchesOnlyOnDescription_ReturnsProductsWhoseDescriptionContainsTheTerm()
        {
            // Arrange
            var term = "Say aloha to this hot mama of a Hawaiian";
            var expected = 11;

            // Act
            var actual = _products.SearchByTerm(term).Single().Id;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SearchByTerm_WhenThereAreMatchesOnNameAndOnDescription_ReturnsProductsWhoseNameOrDescriptionContainsTheTerm()
        {
            // Arrange
            var term = "chicken";
            var expected = 2;

            // Act
            var filteredProducts = _products.SearchByTerm(term);
            var actual = filteredProducts.Count();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
