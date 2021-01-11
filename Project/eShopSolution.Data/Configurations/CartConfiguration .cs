using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.Data.Configuration
{
    class AppConfigConfiguration:IConventionEntityTypeBuilder<Cart>
    {
    }
}
