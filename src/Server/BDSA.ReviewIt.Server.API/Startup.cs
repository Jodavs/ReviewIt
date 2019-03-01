using BDSA.ReviewIt.Server.Logic.StudyManager;
using BDSA.ReviewIt.Server.Logic.TaskManager;
using BDSA.ReviewIt.Server.Logic.UserManager;
using BDSA.ReviewIt.Server.Logic.UserManager.Interfaces;
using BDSA.ReviewIt.Server.Logic.Utilities;
using BDSA.ReviewIt.Server.Logic.Utilities.Interfaces;
using BDSA.ReviewIt.Server.StorageLayer;
using BDSA.ReviewIt.Server.StorageLayer.Repositories;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using RepositoryLayer.Repositories;
using ServerDTOs.ServerDTOs;
using Swashbuckle.Swagger.Model;

namespace Server
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<IPublicationLogic, PublicationLogic>();
            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<IStudyLogic, StudyLogic>();
            services.AddTransient<IClassificationLogic, ClassificationLogic>();
            services.AddTransient<IExclusionLogic, ExclusionLogic>();
            services.AddTransient<IPhaseLogic, PhaseLogic>();
            services.AddTransient<ITaskLogic, TaskLogic>();
            services.AddTransient<IFieldLogic, FieldLogic>();
            services.AddTransient<IImportLogic, ImportLogic>();
            services.AddTransient<IExportLogic, ExportLogic>();

            services.AddSingleton<IRepository<UserDTO>, UserRepository>();
            services.AddSingleton<IRepository<StudyDTO>, StudyRepository>();
            services.AddSingleton<IRepository<ParticipantDTO>, ParticipantRepository>();
            services.AddSingleton<IRepository<AnswerDTO>, AnswerRepository>();
            services.AddSingleton<IRepository<ClassificationCriterionDTO>, ClassificationCriterionRepository>();
            services.AddSingleton<IRepository<DataDTO>, DataRepository>();
            services.AddSingleton<IRepository<ExclusionCriterionDTO>, ExclusionCriterionRepository>();
            services.AddSingleton<IRepository<FieldDTO>, FieldRepository>();
            services.AddSingleton<IRepository<PhaseDTO>, PhaseRepository>();
            services.AddSingleton<IRepository<ReviewTaskDTO>, TaskRepository>();
            services.AddSingleton<IRepository<StudyDTO>, StudyRepository>();
            services.AddSingleton<IRepository<TaskDelegationDTO>, TaskDelegationRepository>();

            services.AddSingleton<EFContext>();

            // Add framework services.
            services.AddMvc();


            // Swagger framework for documentation
            services.AddSwaggerGen();

            // TODO: Taken from Swagger setup page https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "ReviewIt API"
                });

                // Determine base path for the application.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                // Set the comments path for the swagger json and ui.
                options.IncludeXmlComments(basePath + "\\BDSA.ReviewIt.Server.API.xml");

                options.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            // Swagger framework for documentation
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}