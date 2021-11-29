WE ARE USING 'IN-MEMORY' DATABASE FOR NOW,
	IN DEVELOPMENT ENVIRONMENT.
WHEN WE MOVE ON TO PRODUCTION ENVIRONMENT
	WE ARE GONNA USE 'SQL SERVER' 'KUBERNETES'

i) model is created
ii) dbcontext (representation of db) is created
iii) dbset is represantation of 'table' 'entity'
iv) added dbcontext to startup 'dependency injection?'


this part is dependency injection
 private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context) // AppDbContext is injected in Startup
        {
            _context = context;
        }

* 'in-memory' database is used for TESTING. If we restart app all data is deleted.