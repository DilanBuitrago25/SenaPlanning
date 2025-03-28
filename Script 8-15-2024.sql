USE [SenaPlanning]
GO
/****** Object:  Table [dbo].[Ambiente]    Script Date: 8/15/2024 5:30:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ambiente](
	[IdAmbiente] [int] IDENTITY(1,1) NOT NULL,
	[NombreAmbiente] [varchar](150) NULL,
	[EstadoAmbiente] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAmbiente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Area_Conocimiento]    Script Date: 8/15/2024 5:30:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area_Conocimiento](
	[IdArea] [int] IDENTITY(1,1) NOT NULL,
	[CodigoArea] [int] NOT NULL,
	[NombreArea] [varchar](255) NOT NULL,
	[EstadoArea] [bit] NOT NULL,
	[IdRed] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Area_conocimiento_has_Instructor]    Script Date: 8/15/2024 5:30:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area_conocimiento_has_Instructor](
	[IdInstructor] [int] NOT NULL,
	[IdArea] [int] NOT NULL,
 CONSTRAINT [PK_Area_conocimiento_has_Instructor] PRIMARY KEY CLUSTERED 
(
	[IdInstructor] ASC,
	[IdArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ficha]    Script Date: 8/15/2024 5:30:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ficha](
	[IdFicha] [int] IDENTITY(1,1) NOT NULL,
	[CodigoFicha] [int] NULL,
	[FechaInFicha] [date] NULL,
	[FechaFinFicha] [date] NULL,
	[NumAprenFicha] [int] NULL,
	[TrimestreFicha] [varchar](25) NULL,
	[JornadaFicha] [varchar](100) NULL,
	[EstadoFicha] [bit] NOT NULL,
	[IdPrograma] [int] NULL,
	[IdOferta] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdFicha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ficha_has_Ambiente]    Script Date: 8/15/2024 5:30:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ficha_has_Ambiente](
	[IdFicha] [int] NOT NULL,
	[IdAmbiente] [int] NOT NULL,
 CONSTRAINT [PK_Ficha_has_Ambiente] PRIMARY KEY CLUSTERED 
(
	[IdFicha] ASC,
	[IdAmbiente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 8/15/2024 5:30:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructor](
	[IdInstructor] [int] IDENTITY(1,1) NOT NULL,
	[DocumentoInstructor] [int] NULL,
	[NombreCompletoInstructor] [varchar](300) NULL,
	[CodRegionalInstructor] [int] NULL,
	[RegionalInstructor] [varchar](255) NULL,
	[CodCCOS] [int] NULL,
	[DependenciaInstructor] [varchar](255) NULL,
	[CargoInstructor] [varchar](150) NULL,
	[TipoCargoInstructor] [varchar](255) NULL,
	[CorreoSENAInstructor] [varchar](255) NULL,
	[RedInstructor] [varchar](150) NULL,
	[AreaInstructor] [varchar](150) NULL,
	[RutaInstructor] [varchar](150) NULL,
	[CodMunicipioInstructor] [int] NULL,
	[MunicipioInstructor] [varchar](150) NULL,
	[FechaNacimientoInstructor] [date] NULL,
	[FechaIngreso] [date] NULL,
	[SexoInstructor] [varchar](150) NULL,
	[EstadoInstructor] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdInstructor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meta]    Script Date: 8/15/2024 5:30:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meta](
	[IdMeta] [int] IDENTITY(1,1) NOT NULL,
	[MetaTecnico] [float] NULL,
	[MetaET] [float] NULL,
	[MetaOtros] [float] NULL,
	[EstadoMeta] [bit] NOT NULL,
	[MetaTGOApPasan] [bigint] NULL,
	[MetaTCOApPasan] [bigint] NULL,
	[MetaETApPasan] [bigint] NULL,
	[MetaOTROApPasan] [bigint] NULL,
	[MetaTecnologia] [bigint] NULL,
	[APorCurso] [int] NOT NULL,
	[APorCursoOtros] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMeta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Oferta]    Script Date: 8/15/2024 5:30:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oferta](
	[IdOferta] [int] IDENTITY(1,1) NOT NULL,
	[CodigoOferta] [int] NULL,
	[HoraReqTrimIOferta] [int] NULL,
	[HoraReqTrimIIOferta] [int] NULL,
	[HoraReqTrimIIIOferta] [int] NULL,
	[HoraReqTrimIVOferta] [int] NULL,
	[CanInstPlantaOferta] [int] NULL,
	[HorasContTrimIOferta] [int] NULL,
	[HorasContTrimIIOferta] [int] NULL,
	[HorasContTrimIIIOferta] [int] NULL,
	[HorasContTrimIVOferta] [int] NULL,
	[CantidadInstContratoTrimIOferta] [int] NULL,
	[CantidadInstContratoTrimIIOferta] [int] NULL,
	[CantidadInstContratoTrimIIIOferta] [int] NULL,
	[CantidadInstContratoTrimIVOferta] [int] NULL,
	[TrimestreProgramadosOferta] [int] NULL,
	[TotalAprendicesOferta] [int] NULL,
	[TotalCursosNuevosOferta] [int] NULL,
	[TotalCursosEPtrimestreOferta] [int] NULL,
	[TotalCursosCursosNuevosOferta] [int] NULL,
	[TotalCursosOferta] [int] NULL,
	[CantidadTrimProgramadosOferta] [int] NULL,
	[CantidadTrimEPOferta] [int] NULL,
	[TotalInstaContratarOferta] [int] NULL,
	[AprenPasanOferta] [int] NULL,
	[AprenProgOferta] [int] NULL,
	[CursoOferta] [float] NULL,
	[EstadoOferta] [bit] NOT NULL,
	[IdUsuario] [int] NULL,
	[IdMetas] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdOferta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Programa_Formacion]    Script Date: 8/15/2024 5:30:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Programa_Formacion](
	[IdPrograma] [int] IDENTITY(1,1) NOT NULL,
	[DenominacionPrograma] [varchar](100) NOT NULL,
	[VersionPrograma] [varchar](100) NOT NULL,
	[NivelPrograma] [varchar](100) NULL,
	[EstadoPrograma] [bit] NOT NULL,
	[IdArea] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPrograma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Red_Conocimiento]    Script Date: 8/15/2024 5:30:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Red_Conocimiento](
	[IdRed] [int] IDENTITY(1,1) NOT NULL,
	[NombreRed] [varchar](150) NOT NULL,
	[CodigoRed] [int] NULL,
	[EstadoRed] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRed] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 8/15/2024 5:30:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[TipoDocumentoUsuario] [varchar](100) NULL,
	[DocumentoUsuario] [int] NULL,
	[NombreUsuario] [varchar](255) NULL,
	[ApellidoUsuario] [varchar](255) NULL,
	[TelefonoUsuario] [int] NULL,
	[ConstraseñaUsuario] [varchar](255) NULL,
	[TipoUsuario] [varchar](255) NULL,
	[EstadoUsuario] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Instructor] ON 

INSERT [dbo].[Instructor] ([IdInstructor], [DocumentoInstructor], [NombreCompletoInstructor], [CodRegionalInstructor], [RegionalInstructor], [CodCCOS], [DependenciaInstructor], [CargoInstructor], [TipoCargoInstructor], [CorreoSENAInstructor], [RedInstructor], [AreaInstructor], [RutaInstructor], [CodMunicipioInstructor], [MunicipioInstructor], [FechaNacimientoInstructor], [FechaIngreso], [SexoInstructor], [EstadoInstructor]) VALUES (1, 3348679, N'MARRUGO LEVYA WILLIAM', 11, N'Bolivar', 9218, N'Centro Para la Industria PetroQuimica', N'Instructor G20', N'Carrera Administrativa', N'wmarrugo@sena.edu.co', N'INSTITUCIÓN DE PEDAGOGÍA', N'Coordinación Académica', N'Coordinación Académica', 13001, N'Cartagena', CAST(N'1956-03-11' AS Date), CAST(N'1993-03-11' AS Date), N'MASCULINO', 1)
SET IDENTITY_INSERT [dbo].[Instructor] OFF
GO
SET IDENTITY_INSERT [dbo].[Meta] ON 

INSERT [dbo].[Meta] ([IdMeta], [MetaTecnico], [MetaET], [MetaOtros], [EstadoMeta], [MetaTGOApPasan], [MetaTCOApPasan], [MetaETApPasan], [MetaOTROApPasan], [MetaTecnologia], [APorCurso], [APorCursoOtros]) VALUES (1, 1567, 0, 205, 1, 2071, 902, 0, 70, 2980, 30, 30)
SET IDENTITY_INSERT [dbo].[Meta] OFF
GO
SET IDENTITY_INSERT [dbo].[Red_Conocimiento] ON 

INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (1, N'PruebaRed1', 1000, 1)
INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (2, N'PruebaRed2', 1001, 0)
INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (3, N'PruebaRed3', 1002, 1)
INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (4, N'PruebaRed4', 1003, 1)
INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (5, N'PruebaRed5', 1004, 1)
INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (6, N'PruebaRed2', 1005, 1)
INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (7, N'PruebaRed2', 54, 1)
INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (8, N'PruebaRed2', 54, 1)
INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (9, N'PruebaRed7', 45, 1)
INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (10, N'PruebaRed2454', 54, 1)
INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (11, N'ffsd', 54, 0)
INSERT [dbo].[Red_Conocimiento] ([IdRed], [NombreRed], [CodigoRed], [EstadoRed]) VALUES (1002, N'Analisis y Red de conocimiento', 12548, 0)
SET IDENTITY_INSERT [dbo].[Red_Conocimiento] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [TipoDocumentoUsuario], [DocumentoUsuario], [NombreUsuario], [ApellidoUsuario], [TelefonoUsuario], [ConstraseñaUsuario], [TipoUsuario], [EstadoUsuario]) VALUES (1, N'C.C.', 1128061463, N'Dilan', N'Buitrago', 21564, N'123', N'Administrador', 1)
INSERT [dbo].[Usuario] ([IdUsuario], [TipoDocumentoUsuario], [DocumentoUsuario], [NombreUsuario], [ApellidoUsuario], [TelefonoUsuario], [ConstraseñaUsuario], [TipoUsuario], [EstadoUsuario]) VALUES (2, N'C.C.', 1002192518, N'Julio', N'Buitrafo', 45648, N'123', N'Coordinador', 1)
INSERT [dbo].[Usuario] ([IdUsuario], [TipoDocumentoUsuario], [DocumentoUsuario], [NombreUsuario], [ApellidoUsuario], [TelefonoUsuario], [ConstraseñaUsuario], [TipoUsuario], [EstadoUsuario]) VALUES (6, N'C.C.', 1041975419, N'Jorge Luis', N'Moreno Lara', 545511, N'123', N'Administrador', 1)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
ALTER TABLE [dbo].[Ambiente] ADD  DEFAULT ((1)) FOR [EstadoAmbiente]
GO
ALTER TABLE [dbo].[Area_Conocimiento] ADD  DEFAULT ((1)) FOR [EstadoArea]
GO
ALTER TABLE [dbo].[Ficha] ADD  DEFAULT ((1)) FOR [EstadoFicha]
GO
ALTER TABLE [dbo].[Instructor] ADD  DEFAULT ((1)) FOR [EstadoInstructor]
GO
ALTER TABLE [dbo].[Meta] ADD  DEFAULT ((1)) FOR [EstadoMeta]
GO
ALTER TABLE [dbo].[Meta] ADD  DEFAULT ((30)) FOR [APorCurso]
GO
ALTER TABLE [dbo].[Meta] ADD  DEFAULT ((30)) FOR [APorCursoOtros]
GO
ALTER TABLE [dbo].[Oferta] ADD  DEFAULT ((1)) FOR [EstadoOferta]
GO
ALTER TABLE [dbo].[Programa_Formacion] ADD  DEFAULT ((1)) FOR [EstadoPrograma]
GO
ALTER TABLE [dbo].[Red_Conocimiento] ADD  DEFAULT ((1)) FOR [EstadoRed]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((1)) FOR [EstadoUsuario]
GO
ALTER TABLE [dbo].[Area_Conocimiento]  WITH CHECK ADD FOREIGN KEY([IdRed])
REFERENCES [dbo].[Red_Conocimiento] ([IdRed])
GO
ALTER TABLE [dbo].[Area_conocimiento_has_Instructor]  WITH CHECK ADD  CONSTRAINT [FK_Area_conocimiento_has_Instructor_IdArea] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Area_Conocimiento] ([IdArea])
GO
ALTER TABLE [dbo].[Area_conocimiento_has_Instructor] CHECK CONSTRAINT [FK_Area_conocimiento_has_Instructor_IdArea]
GO
ALTER TABLE [dbo].[Area_conocimiento_has_Instructor]  WITH CHECK ADD  CONSTRAINT [FK_Area_conocimiento_has_Instructor_IdInstructor] FOREIGN KEY([IdInstructor])
REFERENCES [dbo].[Instructor] ([IdInstructor])
GO
ALTER TABLE [dbo].[Area_conocimiento_has_Instructor] CHECK CONSTRAINT [FK_Area_conocimiento_has_Instructor_IdInstructor]
GO
ALTER TABLE [dbo].[Ficha]  WITH CHECK ADD FOREIGN KEY([IdOferta])
REFERENCES [dbo].[Oferta] ([IdOferta])
GO
ALTER TABLE [dbo].[Ficha]  WITH CHECK ADD FOREIGN KEY([IdPrograma])
REFERENCES [dbo].[Programa_Formacion] ([IdPrograma])
GO
ALTER TABLE [dbo].[Ficha_has_Ambiente]  WITH CHECK ADD  CONSTRAINT [FK_Ficha_has_Ambiente_IdAmbiente] FOREIGN KEY([IdAmbiente])
REFERENCES [dbo].[Ambiente] ([IdAmbiente])
GO
ALTER TABLE [dbo].[Ficha_has_Ambiente] CHECK CONSTRAINT [FK_Ficha_has_Ambiente_IdAmbiente]
GO
ALTER TABLE [dbo].[Ficha_has_Ambiente]  WITH CHECK ADD  CONSTRAINT [FK_Ficha_has_Ambiente_IdFicha] FOREIGN KEY([IdFicha])
REFERENCES [dbo].[Ficha] ([IdFicha])
GO
ALTER TABLE [dbo].[Ficha_has_Ambiente] CHECK CONSTRAINT [FK_Ficha_has_Ambiente_IdFicha]
GO
ALTER TABLE [dbo].[Oferta]  WITH CHECK ADD FOREIGN KEY([IdMetas])
REFERENCES [dbo].[Meta] ([IdMeta])
GO
ALTER TABLE [dbo].[Oferta]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Programa_Formacion]  WITH CHECK ADD FOREIGN KEY([IdArea])
REFERENCES [dbo].[Area_Conocimiento] ([IdArea])
GO
