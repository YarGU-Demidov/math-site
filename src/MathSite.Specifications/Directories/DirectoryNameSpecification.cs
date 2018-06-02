using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Directories
{
    public class DirectoryNameSpecification : Specification<Directory>
    {
        private readonly string _directoryName;

        public DirectoryNameSpecification(string directoryName)
        {
            _directoryName = directoryName;
        }

        public override Expression<Func<Directory, bool>> ToExpression()
        {
            return directory => directory.Name == _directoryName;
        }
    }
}