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


*** WHAT ARE THESE DTOs(Data Transfer Objects)?
    Ans: External represantation of the internal models
*** WHY DO WE WANNA USE DTOs?
    Ans: 
        i) For not to expose internal models to external consumers.
            -- Data privacy : we do not want to make visinle some data (fields)
            -- Contructual coupling : if we want to change our internal model (db table fields) it may break cunsomers code
                                      unless we do not use DTOs (in an API for example)

Internal(model) and external(dto) classes have no idea about themselves, they are seperate classses
we do make it via automapper.profile
Using AutoMapper we 'map' one to another, bit it works one way, for example model -> dto mappimg does not map dto -> model 



