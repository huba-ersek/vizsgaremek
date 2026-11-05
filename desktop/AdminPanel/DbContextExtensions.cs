using Microsoft.EntityFrameworkCore;

namespace AdminPanel
{
    internal static class DbContextExtensions
    {
        public static dynamic? Set(this DbContext ctx, Type type)
        {
            var method = ctx.GetType().GetMethod(nameof(DbContext.Set), []);
            method = method!.MakeGenericMethod(type);
            return method.Invoke(ctx, null);
        }
    }
}
