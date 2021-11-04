using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReorderDatabase.Data.Extensions
{
    public static class ReorderDatabaseExtensions
    {
        public static IServiceCollection AddReorderDatabase(this IServiceCollection services, Action<Options.ApplicationOptions> options)
        {
            var opt = new Options.ApplicationOptions();
            options(opt);

            return services
                .AddDbContext<NoteContext>(options =>
                {
                    options.UseSqlServer(opt.ConnectionString);
                })
                .AddScoped<Repositories.INoteRepository, Repositories.NoteRepository>()
                .AddScoped<Services.INoteService, Services.NoteService>();
        }
    }
}
