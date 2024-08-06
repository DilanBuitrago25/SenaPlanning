CREATE TABLE Ambiente (
  idAmbiente INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  Jornada_idJornada INTEGER UNSIGNED NOT NULL,
  nombreAmbiente VARCHAR(45) NULL,
  PRIMARY KEY(idAmbiente, Jornada_idJornada),
  INDEX Ambiente_FKIndex1(Jornada_idJornada)
);

CREATE TABLE Competencias (
  idCompetencias INT NOT NULL AUTO_INCREMENT,
  Programas_formacion_Red_conocimiento_idRed_conocimiento INTEGER UNSIGNED NOT NULL,
  Programas_formacion_codigoProgramas_formacion INTEGER UNSIGNED NOT NULL,
  TipoCompetencia _codigoTipoCompetencia INTEGER UNSIGNED NOT NULL,
  nombreCompetencia VARCHAR(255) NOT NULL,
  PRIMARY KEY(idCompetencias, Programas_formacion_Red_conocimiento_idRed_conocimiento, Programas_formacion_codigoProgramas_formacion),
  INDEX Competencias_FKIndex1(Programas_formacion_codigoProgramas_formacion, Programas_formacion_Red_conocimiento_idRed_conocimiento),
  INDEX Competencias_FKIndex2(TipoCompetencia _codigoTipoCompetencia)
);

CREATE TABLE Fichas (
  codigoFichas INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  Programas_formacion_Red_conocimiento_idRed_conocimiento INTEGER UNSIGNED NOT NULL,
  Programas_formacion_codigoProgramas_formacion INTEGER UNSIGNED NOT NULL,
  Oferta_codigoOferta INTEGER UNSIGNED NOT NULL,
  Jornada_idJornada INTEGER UNSIGNED NOT NULL,
  fechaInFicha VARCHAR(30) NULL,
  fechaFinFicha VARCHAR(30) NULL,
  numaprenFicha INT NULL,
  trimestreFicha VARCHAR(15) NULL,
  PRIMARY KEY(codigoFichas, Programas_formacion_Red_conocimiento_idRed_conocimiento, Programas_formacion_codigoProgramas_formacion),
  INDEX Fichas_FKIndex1(Programas_formacion_codigoProgramas_formacion, Programas_formacion_Red_conocimiento_idRed_conocimiento),
  INDEX Fichas_FKIndex2(Jornada_idJornada),
  INDEX Fichas_FKIndex3(Oferta_codigoOferta)
);

CREATE TABLE Instructor (
  idInstructor INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  nombresInstructor VARCHAR(45) NULL,
  PRIMARY KEY(idInstructor)
);

CREATE TABLE Jornada (
  idJornada INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  tipoJornada VARCHAR(20) NULL,
  PRIMARY KEY(idJornada)
);

CREATE TABLE Oferta (
  codigoOferta INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  horareqtrimI INT NULL,
  horareqtrmII INT NULL,
  horareqtrimIII INT NULL,
  horareqtrimIV INT NULL,
  canInsPlanta INT NULL,
  horascontTrimI INT NULL,
  horasconTrimII INT NULL,
  horasconTrimIII INT NULL,
  horasconTrimIV INT NULL,
  cantidadInstContratoItrm INT NULL,
  cantidadInstContratoIItrim INT NULL,
  cantidadInstContratoIIItrim INT NULL,
  cantidadInsCobtratoIVtrim INT NULL,
  trimestresprogramados INT NULL,
  totalaprendices INT NULL,
  totalcursosnuevos INT NULL,
  totalcursosEPtrimestres INT NULL,
  totalcursosportrimestre INT NULL,
  totalcursos INT NULL,
  cantidadtirmprogramados INT NULL,
  cantidadtrimEP INT NULL,
  totalInsacontratar INT NULL,
  PRIMARY KEY(codigoOferta)
);

CREATE TABLE Programas_formacion (
  codigoProgramas_formacion INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  Red_conocimiento_idRed_conocimiento INTEGER UNSIGNED NOT NULL,
  denominacProgf VARCHAR(60) NULL,
  versionProgf CHAR NULL,
  nivelProgf VARCHAR(30) NULL,
  PRIMARY KEY(codigoProgramas_formacion, Red_conocimiento_idRed_conocimiento),
  INDEX Programas_formacion_FKIndex1(Red_conocimiento_idRed_conocimiento)
);

CREATE TABLE Red_conocimiento (
  idRed_conocimiento INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  nombreRed_conoc VARCHAR(100) NULL,
  PRIMARY KEY(idRed_conocimiento)
);

CREATE TABLE TipoCompetencia  (
  codigoTipoCompetencia INTEGER UNSIGNED NOT NULL,
  nombreTipoCompetencia VARCHAR(255) NULL,
  perfilCompeTecnica VARCHAR(255) NULL,
  duraciónCompetencia INT NULL,
  PRIMARY KEY(codigoTipoCompetencia)
);

CREATE TABLE TipoCompetencia _has_Instructor (
  TipoCompetencia _codigoTipoCompetencia INTEGER UNSIGNED NOT NULL,
  Instructor_idInstructor INTEGER UNSIGNED NOT NULL,
  PRIMARY KEY(TipoCompetencia _codigoTipoCompetencia, Instructor_idInstructor),
  INDEX TipoCompetencia _has_Instructor_FKIndex1(TipoCompetencia _codigoTipoCompetencia),
  INDEX TipoCompetencia _has_Instructor_FKIndex2(Instructor_idInstructor)
);


