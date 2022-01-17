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


ControllerBase :     A base class for an MVC controller without view support.


REST Spec: Anytime an entity/resource is created you should return back that entity with http code 201. ( --> CreatePlatform returns back PlatformReadDto)

--> INTRODUCTION TO DOCKER CONTAINERS [https://docs.docker.com/samples/dotnetcore/]

*** How the docker engine builds an image?
    Ans: It takes the docker file and then actually uses an empty container to start layering up and building up your resulting image

    i) Image is the blueprint/template (OOP: a class) ; Container (OOP: an instance of that class) 
       Image is used to create a Container
    ii) BUT ALSO An Image is part of the build process 
What docker does is it will take an empty container and build inside that at each step and ultimately you'll end up with an image 

Each step in the docker file a container will be used to perform that step

--

we are going to create a synchronous client that will allow us to talk to our command service

http client factory: it basically gives us an http client, main using of http client factory is if you are 
making multiple requests  using http clients you should be using a factory to do that, 
because it manages connection safety etc. you don't end up with connection exhaustion etc.

with this commit we created a synchronous client that will allow us to talk to our command service

i) we created an interface for http data client and it's concrete class.
ii) we wrote a method (SendPlatformToCommand) to send platforms which is we create (-> PlatformService:CreatePlatform)
iii) we used PlatformReadDto as an argument for SendPlatformToCommand, because we possibly need an id within CommandService.
iv) we used http client for to send our platforms to CommandService, we set up constructor depepndency injection (HttpCommandDataClient & Startup)
v) we put the uri for CommandService to config file so PlatformService is able to know where the CommandService sits