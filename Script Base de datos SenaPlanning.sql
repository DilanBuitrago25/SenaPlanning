Create database SenaPlanning

go

use SenaPlanning

go

create table Red_Conocimiento(
IdRed int identity (1,1)not null,
NombreRed varchar (150) not null,
CodigoRed int,
EstadoRed bit default (1) not null,
primary key (IdRed));

go

create table Meta(
IdMeta int identity (1,1) not null,
MetaTecnologia bigint,
MetaTecnico bigint,
MetaET bigint,
MetaOtros bigint,
EstadoMeta bit default (1) not null,
MetaTGOApPasan bigint,
MetaTCOApPasan bigint,
MetaETApPasan bigint,
MetaOTROApPasan bigint
primary key (IdMeta));
go

create table Usuario(
IdUsuario int identity (1,1) not null,
TipoDocumentoUsuario Varchar (100),
DocumentoUsuario int,
NombreUsuario varchar (255),
ApellidoUsuario varchar (255),
TelefonoUsuario int,
ConstraseñaUsuario varchar (255),
TipoUsuario varchar (255),
EstadoUsuario bit default (1) not null,
primary key (IdUsuario));

go

Insert into Usuario (TipoDocumentoUsuario, DocumentoUsuario, NombreUsuario, ApellidoUsuario, TelefonoUsuario ,ConstraseñaUsuario, TipoUsuario) values
('C.C', '000000', 'Admin', 'Soy Sena',123456,'admin123' ,'Administrador');

go

create table Area_Conocimiento(
IdArea int identity (1,1) not null,
CodigoArea int not null,
NombreArea varchar (255) not null,
EstadoArea bit default (1) not null,
primary key (IdArea),
IdRed int references Red_Conocimiento(IdRed));

go

create table Programa_Formacion(
IdPrograma int identity (1,1) not null,
DenominacionPrograma varchar (100) not null,
VersionPrograma varchar (100) not null,
NivelPrograma varchar (100),
EstadoPrograma bit default (1) not null,
primary key (IdPrograma),
IdArea int references Area_Conocimiento(IdArea));

go

create table Oferta(
IdOferta int identity (1,1) not null,
CodigoOferta int,
HoraReqTrimIOferta int,
HoraReqTrimIIOferta int,
HoraReqTrimIIIOferta int,
HoraReqTrimIVOferta int,
CanInstPlantaOferta int,
HorasContTrimIOferta int,
HorasContTrimIIOferta int,
HorasContTrimIIIOferta int,
HorasContTrimIVOferta int,
CantidadInstContratoTrimIOferta int,
CantidadInstContratoTrimIIOferta int,
CantidadInstContratoTrimIIIOferta int,
CantidadInstContratoTrimIVOferta int,
TrimestreProgramadosOferta int,
TotalAprendicesOferta int,
TotalCursosNuevosOferta int,
TotalCursosEPtrimestreOferta int,
TotalCursosCursosNuevosOferta int,
TotalCursosOferta int,
CantidadTrimProgramadosOferta int,
CantidadTrimEPOferta int,
TotalInstaContratarOferta int,
AprenPasanOferta int,
AprenProgOferta int,
CursoOferta float,
EstadoOferta bit default (1) not null,
primary key (IdOferta),
IdUsuario int references Usuario(IdUsuario),
IdMetas int references Meta(IdMeta));

go

create table Instructor(
IdInstructor int identity (1,1) not null,
DocumentoInstructor int,
NombreCompletoInstructor varchar(300),
CodRegionalInstructor int,
RegionalInstructor varchar (255),
CodCCOS int,
DependenciaInstructor varchar(255),
CargoInstructor varchar (150),
TipoCargoInstructor varchar (255),
CorreoSENAInstructor varchar (255),
RedInstructor varchar (150),
AreaInstructor varchar (150),
RutaInstructor varchar (150),
CodMunicipioInstructor int,
MunicipioInstructor varchar (150),
FechaNacimientoInstructor date,
FechaIngreso date,
SexoInstructor varchar (150),
EstadoInstructor bit default (1) not null,
primary key (IdInstructor));

go

create table Area_conocimiento_has_Instructor(
IdInstructor int,
IdArea int,
CONSTRAINT PK_Area_conocimiento_has_Instructor PRIMARY KEY (IdInstructor, IdArea),
CONSTRAINT FK_Area_conocimiento_has_Instructor_IdArea FOREIGN KEY (IdArea) references Area_Conocimiento(IdArea),
CONSTRAINT FK_Area_conocimiento_has_Instructor_IdInstructor FOREIGN KEY (IdInstructor) references Instructor(IdInstructor)
);

go

create table Ficha(
IdFicha int identity (1,1) not null,
CodigoFicha int,
FechaInFicha date,
FechaFinFicha date,
NumAprenFicha int,
TrimestreFicha Varchar (25),
JornadaFicha Varchar (100),
primary key (IdFicha),
EstadoFicha bit default (1) not null,
IdPrograma int references Programa_Formacion(IdPrograma),
IdOferta int references Oferta(IdOferta));

go

create table Ambiente(
IdAmbiente int identity (1,1) not null,
NombreAmbiente Varchar (150),
EstadoAmbiente bit default (1) not null,
primary key (IdAmbiente));

go

create table Ficha_has_Ambiente(
IdFicha int,
IdAmbiente int,
CONSTRAINT PK_Ficha_has_Ambiente PRIMARY KEY (IdFicha, IdAmbiente),
CONSTRAINT FK_Ficha_has_Ambiente_IdFicha FOREIGN KEY (IdFicha) references Ficha(IdFicha),
CONSTRAINT FK_Ficha_has_Ambiente_IdAmbiente FOREIGN KEY (IdAmbiente) references Ambiente(IdAmbiente)
);


