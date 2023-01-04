using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PolizaJuridica.Migrations
{
    public partial class basic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AreaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreaDescripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AreaId);
                });

            migrationBuilder.CreateTable(
                name: "CentroCostos",
                columns: table => new
                {
                    CentroCostosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CentroCostosTipo = table.Column<string>(nullable: true),
                    CentroCostosMonto = table.Column<decimal>(nullable: false),
                    CentroCostosRentaInicial = table.Column<int>(nullable: false),
                    CentroCostosRentaFinal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroCostos", x => x.CentroCostosId);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    EstadosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstadoNombre = table.Column<string>(nullable: true),
                    CodigoPostal = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.EstadosId);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    LogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogPantalla = table.Column<string>(nullable: true),
                    LogProceso = table.Column<string>(nullable: true),
                    LogObjetoIn = table.Column<string>(nullable: true),
                    LogObjetoOut = table.Column<string>(nullable: true),
                    LogFecha = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "Parametros",
                columns: table => new
                {
                    ParametrosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParametroNombre = table.Column<string>(nullable: true),
                    ParametroValor = table.Column<string>(nullable: true),
                    ParametroValorNumerico = table.Column<string>(nullable: true),
                    ParametroValor1 = table.Column<string>(nullable: true),
                    ParametroValor2 = table.Column<string>(nullable: true),
                    ParametroValor3 = table.Column<string>(nullable: true),
                    ParametroValor4 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametros", x => x.ParametrosId);
                });

            migrationBuilder.CreateTable(
                name: "PolizaEstatus",
                columns: table => new
                {
                    PolizaEstatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolizaEstatus", x => x.PolizaEstatusId);
                });

            migrationBuilder.CreateTable(
                name: "Representacion",
                columns: table => new
                {
                    RepresentacionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RepresentacionNombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representacion", x => x.RepresentacionId);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    TipoDocumentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoDocumentoDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.TipoDocumentoId);
                });

            migrationBuilder.CreateTable(
                name: "TipoInmobiliario",
                columns: table => new
                {
                    TipoInmobiliarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoInmobiliarioDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoInmobiliario", x => x.TipoInmobiliarioId);
                });

            migrationBuilder.CreateTable(
                name: "TipoParentesco",
                columns: table => new
                {
                    TipoParentescoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoParentescoDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoParentesco", x => x.TipoParentescoId);
                });

            migrationBuilder.CreateTable(
                name: "TipoRefComercial",
                columns: table => new
                {
                    TipoRefComercialId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoRepresentaRC = table.Column<string>(nullable: true),
                    TipoDetalleRC = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRefComercial", x => x.TipoRefComercialId);
                });

            migrationBuilder.CreateTable(
                name: "TipoRefPersonal",
                columns: table => new
                {
                    TipoRefPersonalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoRefPersonalDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRefPersonal", x => x.TipoRefPersonalId);
                });

            migrationBuilder.CreateTable(
                name: "MenuP",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Controlador = table.Column<string>(nullable: true),
                    Pantalla = table.Column<string>(nullable: true),
                    MenuPPadreId = table.Column<int>(nullable: true),
                    AreaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuP_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuP_MenuP_MenuPPadreId",
                        column: x => x.MenuPPadreId,
                        principalTable: "MenuP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentoPlantilla",
                columns: table => new
                {
                    DocumentoPlantillaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentoPlantillaTipo = table.Column<string>(nullable: true),
                    DocumentoPlantillaNombre = table.Column<string>(nullable: true),
                    DocumentoPlantillaXml = table.Column<string>(nullable: true),
                    DocumentoOriginal = table.Column<string>(nullable: true),
                    DocumentoPagare = table.Column<bool>(nullable: false),
                    DocumentoFiador = table.Column<bool>(nullable: false),
                    DocumentoInmueble = table.Column<bool>(nullable: false),
                    DocumentoCarta = table.Column<bool>(nullable: false),
                    EstadosId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentoPlantilla", x => x.DocumentoPlantillaId);
                    table.ForeignKey(
                        name: "FK_DocumentoPlantilla_Estados_EstadosId",
                        column: x => x.EstadosId,
                        principalTable: "Estados",
                        principalColumn: "EstadosId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuariosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsurioEmail = table.Column<string>(nullable: true),
                    UsuarioNombre = table.Column<string>(nullable: true),
                    UsuarioApellidoPaterno = table.Column<string>(nullable: true),
                    UsuarioApellidoMaterno = table.Column<string>(nullable: true),
                    UsuarioTelefono = table.Column<string>(nullable: true),
                    UsuarioContrasenia = table.Column<string>(nullable: true),
                    UsuarioNomCompleto = table.Column<string>(nullable: true),
                    UsuarioInmobiliaria = table.Column<string>(nullable: true),
                    AreaId = table.Column<int>(nullable: false),
                    RepresentacionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuariosId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Representacion_RepresentacionId",
                        column: x => x.RepresentacionId,
                        principalTable: "Representacion",
                        principalColumn: "RepresentacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solicitud",
                columns: table => new
                {
                    SolicitudId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SolicitudTipoPoliza = table.Column<string>(nullable: true),
                    SolicitudSolicitudNombreCompania = table.Column<string>(nullable: true),
                    SolicitudPersonaSolicita = table.Column<string>(nullable: true),
                    SolicitudTelefono = table.Column<string>(nullable: true),
                    SolicitudCelular = table.Column<string>(nullable: true),
                    SolicitudEmail = table.Column<string>(nullable: true),
                    SolicitudFechaSolicitud = table.Column<DateTime>(nullable: false),
                    SolicitudFechaFirma = table.Column<DateTime>(nullable: false),
                    SolicitudHoraFirma = table.Column<DateTime>(nullable: false),
                    SolicitudLugarFirma = table.Column<string>(nullable: true),
                    SolicitudAdmiInmueble = table.Column<bool>(nullable: false),
                    SolicitudEsAdminInmueble = table.Column<bool>(nullable: false),
                    SolicitudRecibodePago = table.Column<string>(nullable: true),
                    SolicitudNombreProp = table.Column<string>(nullable: true),
                    SolicitudApePaternoProp = table.Column<string>(nullable: true),
                    SolicitudApeMaternoProp = table.Column<string>(nullable: true),
                    SolicitudNacionalidad = table.Column<string>(nullable: true),
                    SolicitudRazonSocial = table.Column<string>(nullable: true),
                    SolicitudRFC = table.Column<string>(nullable: true),
                    SolicitudRepresentanteLegal = table.Column<string>(nullable: true),
                    SolicitudApePaternoLegal = table.Column<string>(nullable: true),
                    SolicitudApeMaternoLegal = table.Column<string>(nullable: true),
                    SolicitudDomicilioProp = table.Column<string>(nullable: true),
                    SolicitudTelefonoProp = table.Column<string>(nullable: true),
                    SolicitudCelularProp = table.Column<string>(nullable: true),
                    SolicitudEmailProp = table.Column<string>(nullable: true),
                    SolicitudTipoDeposito = table.Column<string>(nullable: true),
                    SolicitudNombreCuenta = table.Column<string>(nullable: true),
                    SolicitudBanco = table.Column<string>(nullable: true),
                    SolicitudCuenta = table.Column<string>(nullable: true),
                    SolicitudCLABE = table.Column<string>(nullable: true),
                    SolicitudUbicacionArrendado = table.Column<string>(nullable: true),
                    SolicitudTelefonoInmueble = table.Column<bool>(nullable: false),
                    SolicitudNumero = table.Column<string>(nullable: true),
                    SolicitudImporteMensual = table.Column<decimal>(nullable: false),
                    SolicitudCuotaMant = table.Column<decimal>(nullable: false),
                    SolicitudIncluidaRenta = table.Column<bool>(nullable: false),
                    SolicitudDepositoGarantia = table.Column<decimal>(nullable: false),
                    SolicitudVigenciaContratoI = table.Column<DateTime>(nullable: false),
                    SolicitudVigenciaContratoF = table.Column<DateTime>(nullable: false),
                    SolicitudPagare = table.Column<bool>(nullable: false),
                    SolicitudDestinoArrendamien = table.Column<string>(nullable: true),
                    SolicitudObservaciones = table.Column<string>(nullable: true),
                    SolicitudEstatus = table.Column<string>(nullable: true),
                    SolicitudTipoRegimen = table.Column<int>(nullable: false),
                    CentroCostosId = table.Column<int>(nullable: false),
                    EstadosId = table.Column<int>(nullable: false),
                    SolicitudInmuebleGaran = table.Column<bool>(nullable: false),
                    SolicitudFiador = table.Column<bool>(nullable: false),
                    SolicitudCartaEntrega = table.Column<bool>(nullable: false),
                    TipoArrendatario = table.Column<string>(nullable: true),
                    ArrendatarioNombre = table.Column<string>(nullable: true),
                    ArrendatarioApePat = table.Column<string>(nullable: true),
                    ArrendatarioApeMat = table.Column<string>(nullable: true),
                    ArrendatarioTelefono = table.Column<string>(nullable: true),
                    ArrendatarioCorreo = table.Column<string>(nullable: true),
                    TipoInmobiliarioId = table.Column<int>(nullable: false),
                    TextoVigenciaFechaIF = table.Column<string>(nullable: true),
                    TextoImporterenta = table.Column<string>(nullable: true),
                    TextoDeposito = table.Column<string>(nullable: true),
                    TextoCuotaGarantia = table.Column<string>(nullable: true),
                    TextoVigenciaFechaI = table.Column<string>(nullable: true),
                    TextoVigenciaFechaF = table.Column<string>(nullable: true),
                    TextoCostoPoliza = table.Column<string>(nullable: true),
                    TextoDiaPosteriorVencimiento = table.Column<string>(nullable: true),
                    DiaPago = table.Column<string>(nullable: true),
                    Representanteid = table.Column<int>(nullable: false),
                    RepresentanteNombre = table.Column<string>(nullable: true),
                    Asesorid = table.Column<int>(nullable: false),
                    AsesorNombre = table.Column<string>(nullable: true),
                    Creadorid = table.Column<int>(nullable: false),
                    CreadorNombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitud", x => x.SolicitudId);
                    table.ForeignKey(
                        name: "FK_Solicitud_CentroCostos_CentroCostosId",
                        column: x => x.CentroCostosId,
                        principalTable: "CentroCostos",
                        principalColumn: "CentroCostosId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solicitud_Estados_EstadosId",
                        column: x => x.EstadosId,
                        principalTable: "Estados",
                        principalColumn: "EstadosId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solicitud_TipoInmobiliario_TipoInmobiliarioId",
                        column: x => x.TipoInmobiliarioId,
                        principalTable: "TipoInmobiliario",
                        principalColumn: "TipoInmobiliarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FisicaMoral",
                columns: table => new
                {
                    FisicaMoralId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SFisicaNacionallidad = table.Column<string>(nullable: true),
                    SFisicaCondMigratoria = table.Column<string>(nullable: true),
                    SFisicaEstadoCivil = table.Column<string>(nullable: true),
                    SFisicaConvenioEC = table.Column<string>(nullable: true),
                    SFisicaDomicilio = table.Column<string>(nullable: true),
                    SFisicaColonia = table.Column<string>(nullable: true),
                    SFisicaDelegacionMunicipio = table.Column<string>(nullable: true),
                    SFisicaEstado = table.Column<int>(nullable: false),
                    SFisicaTelefono = table.Column<string>(nullable: true),
                    SFisicaCelular = table.Column<string>(nullable: true),
                    SFisicaEmail = table.Column<string>(nullable: true),
                    SFisicaProfesion = table.Column<string>(nullable: true),
                    SFisicaIngresoMensual = table.Column<decimal>(nullable: false),
                    SFisicaTrabajo = table.Column<string>(nullable: true),
                    SFisicaAntiguedad = table.Column<int>(nullable: false),
                    SFisicaPuesto = table.Column<string>(nullable: true),
                    SFisicaTelefonoTrabajo = table.Column<string>(nullable: true),
                    SFisicaHorario = table.Column<string>(nullable: true),
                    SFisicaDomicilioTrabajo = table.Column<string>(nullable: true),
                    SFisicaColoniaTrabajo = table.Column<string>(nullable: true),
                    SFisicaDelegMuniTrabajo = table.Column<string>(nullable: true),
                    SFisicaEstadoTrabajo = table.Column<int>(nullable: false),
                    SFisicaGiroTrabajo = table.Column<string>(nullable: true),
                    SFisicaWebTrabajo = table.Column<string>(nullable: true),
                    SFisicaJefeTrabajo = table.Column<string>(nullable: true),
                    SFisicaPuestoJefe = table.Column<string>(nullable: true),
                    SFisicaEmailJefe = table.Column<string>(nullable: true),
                    SFisicaFactura = table.Column<bool>(nullable: false),
                    ActaConstitutiva = table.Column<string>(nullable: true),
                    PoderRepresentanteNo = table.Column<string>(nullable: true),
                    DomicilioRepresentanteLegal = table.Column<string>(nullable: true),
                    ColoniaRL = table.Column<string>(nullable: true),
                    DeleMuni = table.Column<string>(nullable: true),
                    TelefonoRL = table.Column<string>(nullable: true),
                    EstadoRL = table.Column<int>(nullable: false),
                    EmailRL = table.Column<string>(nullable: true),
                    HorarioRL = table.Column<string>(nullable: true),
                    IngresoMensualRL = table.Column<decimal>(nullable: false),
                    SindicadoRL = table.Column<string>(nullable: true),
                    RequiereFacturaRL = table.Column<bool>(nullable: false),
                    AfianzadoRL = table.Column<bool>(nullable: false),
                    AfianzadoraRL = table.Column<string>(nullable: true),
                    SolicitudId = table.Column<int>(nullable: false),
                    SFisicaNombre = table.Column<string>(nullable: true),
                    SFisicaApePat = table.Column<string>(nullable: true),
                    SFisicaApeMat = table.Column<string>(nullable: true),
                    SFisicaRFC = table.Column<string>(nullable: true),
                    SFisicaRazonSocial = table.Column<string>(nullable: true),
                    SFisicaCodigoPostal = table.Column<string>(nullable: true),
                    SFisicaCodigoPostalTrabajo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FisicaMoral", x => x.FisicaMoralId);
                    table.ForeignKey(
                        name: "FK_FisicaMoral_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitud",
                        principalColumn: "SolicitudId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosSolicitud",
                columns: table => new
                {
                    usuariosSolicitudId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuariosId = table.Column<int>(nullable: false),
                    SolicitudId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Proceso = table.Column<string>(nullable: true),
                    Observacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosSolicitud", x => x.usuariosSolicitudId);
                    table.ForeignKey(
                        name: "FK_UsuariosSolicitud_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitud",
                        principalColumn: "SolicitudId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosSolicitud_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuariosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calendario",
                columns: table => new
                {
                    CalendarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalendarioFechaFirma = table.Column<DateTime>(nullable: false),
                    CalendarioUbicación = table.Column<string>(nullable: true),
                    CalendarioEstatus = table.Column<string>(nullable: true),
                    CalendarioDescripcion = table.Column<string>(nullable: true),
                    UsuariosId = table.Column<int>(nullable: true),
                    FisicaMoralId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendario", x => x.CalendarioId);
                    table.ForeignKey(
                        name: "FK_Calendario_FisicaMoral_FisicaMoralId",
                        column: x => x.FisicaMoralId,
                        principalTable: "FisicaMoral",
                        principalColumn: "FisicaMoralId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calendario_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuariosId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    DocumentosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentosImagen = table.Column<string>(nullable: true),
                    DocumentoDesc = table.Column<string>(nullable: true),
                    TipoDocumentoId = table.Column<int>(nullable: false),
                    FisicaMoralId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.DocumentosId);
                    table.ForeignKey(
                        name: "FK_Documentos_FisicaMoral_FisicaMoralId",
                        column: x => x.FisicaMoralId,
                        principalTable: "FisicaMoral",
                        principalColumn: "FisicaMoralId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documentos_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumento",
                        principalColumn: "TipoDocumentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FiadorF",
                columns: table => new
                {
                    FiadorFId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FiadorFNombres = table.Column<string>(nullable: true),
                    FiadorFApePaterno = table.Column<string>(nullable: true),
                    FiadorFApeMaterno = table.Column<string>(nullable: true),
                    FiadorFNacionalidad = table.Column<string>(nullable: true),
                    FiadorFCondicionMigratoria = table.Column<string>(nullable: true),
                    FiadorFParentesco = table.Column<string>(nullable: true),
                    FiadorFEstadoCivil = table.Column<string>(nullable: true),
                    FiadorFConvenioEC = table.Column<string>(nullable: true),
                    FiadorFDomicilio = table.Column<string>(nullable: true),
                    FiadorFColonia = table.Column<string>(nullable: true),
                    FiadorFDelegacion = table.Column<string>(nullable: true),
                    FiadorFEstado = table.Column<string>(nullable: true),
                    FiadorFCodigoPostal = table.Column<int>(nullable: false),
                    FiadorFTelefono = table.Column<string>(nullable: true),
                    FiadorFCelular = table.Column<string>(nullable: true),
                    FiadorFEmail = table.Column<string>(nullable: true),
                    FiadorFDomicilioGarantia = table.Column<string>(nullable: true),
                    FiadorFColoniaGarantia = table.Column<string>(nullable: true),
                    FiadorFDelegaciónGarantia = table.Column<string>(nullable: true),
                    FiadorFEstadoGarantia = table.Column<string>(nullable: true),
                    FiadorFProfesion = table.Column<string>(nullable: true),
                    FiadorFEmpresa = table.Column<string>(nullable: true),
                    FiadorFTelefonoEmpresa = table.Column<string>(nullable: true),
                    FiadorFCodigoPostalGarantia = table.Column<int>(nullable: false),
                    FiadorFNombresConyuge = table.Column<string>(nullable: true),
                    FiadorFApePaternoConyuge = table.Column<string>(nullable: true),
                    FiadorFApeMaternoConyuge = table.Column<string>(nullable: true),
                    FisicaMoralId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiadorF", x => x.FiadorFId);
                    table.ForeignKey(
                        name: "FK_FiadorF_FisicaMoral_FisicaMoralId",
                        column: x => x.FisicaMoralId,
                        principalTable: "FisicaMoral",
                        principalColumn: "FisicaMoralId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FiadorM",
                columns: table => new
                {
                    FiadorMId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FiaddorMRazonSocial = table.Column<string>(nullable: true),
                    FiadorMRFC = table.Column<string>(nullable: true),
                    FiadorMDomicilio = table.Column<string>(nullable: true),
                    FiadorMColonia = table.Column<string>(nullable: true),
                    FiadorMDelegacion = table.Column<string>(nullable: true),
                    FiadorMTelefono = table.Column<string>(nullable: true),
                    FiadorMGiro = table.Column<string>(nullable: true),
                    FiadorMWeb = table.Column<string>(nullable: true),
                    FiadorMNombresRLegal = table.Column<string>(nullable: true),
                    FiadorMApePaternoRLegal = table.Column<string>(nullable: true),
                    FiadorMApeMaternoRLegal = table.Column<string>(nullable: true),
                    FiadorMPuestoRLegal = table.Column<string>(nullable: true),
                    FiadorMNacionalidadRLegal = table.Column<string>(nullable: true),
                    FiadorMNCMRlegal = table.Column<string>(nullable: true),
                    FiadorMMActaNo = table.Column<string>(nullable: true),
                    FiadorMPoderRepNo = table.Column<string>(nullable: true),
                    FiadorMDomicilioRLegal = table.Column<string>(nullable: true),
                    FiadorMColoniaRLegal = table.Column<string>(nullable: true),
                    FiadorMDelegacionRLegal = table.Column<string>(nullable: true),
                    FiadorMTelefonoRLegat = table.Column<string>(nullable: true),
                    FiadorMCelularRLegal = table.Column<string>(nullable: true),
                    FiadorMEmailRLegal = table.Column<string>(nullable: true),
                    FiadorMDomicilioGarantia = table.Column<string>(nullable: true),
                    FiadorMColiniaGarantia = table.Column<string>(nullable: true),
                    FiadorMDelegacionGarantia = table.Column<string>(nullable: true),
                    FisicaMoralId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiadorM", x => x.FiadorMId);
                    table.ForeignKey(
                        name: "FK_FiadorM_FisicaMoral_FisicaMoralId",
                        column: x => x.FisicaMoralId,
                        principalTable: "FisicaMoral",
                        principalColumn: "FisicaMoralId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonasOcupanInm",
                columns: table => new
                {
                    PersonasOcupanInmId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonasOcupanInmNombre = table.Column<string>(nullable: true),
                    TipoParentescoId = table.Column<int>(nullable: false),
                    FisicaMoralId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonasOcupanInm", x => x.PersonasOcupanInmId);
                    table.ForeignKey(
                        name: "FK_PersonasOcupanInm_FisicaMoral_FisicaMoralId",
                        column: x => x.FisicaMoralId,
                        principalTable: "FisicaMoral",
                        principalColumn: "FisicaMoralId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonasOcupanInm_TipoParentesco_TipoParentescoId",
                        column: x => x.TipoParentescoId,
                        principalTable: "TipoParentesco",
                        principalColumn: "TipoParentescoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefArrendamiento",
                columns: table => new
                {
                    RefArrendamientoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RefArrenNombres = table.Column<string>(nullable: true),
                    RefArrenApePaterno = table.Column<string>(nullable: true),
                    RefArrenApeMaterno = table.Column<string>(nullable: true),
                    RefArrenTelefono = table.Column<string>(nullable: true),
                    RefArrenDomicilio = table.Column<string>(nullable: true),
                    RefArrenMonto = table.Column<decimal>(nullable: false),
                    RefArrenMotivoCambio = table.Column<string>(nullable: true),
                    FisicaMoralId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefArrendamiento", x => x.RefArrendamientoId);
                    table.ForeignKey(
                        name: "FK_RefArrendamiento_FisicaMoral_FisicaMoralId",
                        column: x => x.FisicaMoralId,
                        principalTable: "FisicaMoral",
                        principalColumn: "FisicaMoralId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferenciaComercial",
                columns: table => new
                {
                    ReferenciaComercialId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RCDetalle = table.Column<string>(nullable: true),
                    RCREpresenta = table.Column<string>(nullable: true),
                    TipoRefComercialId = table.Column<int>(nullable: false),
                    FisicaMoralId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenciaComercial", x => x.ReferenciaComercialId);
                    table.ForeignKey(
                        name: "FK_ReferenciaComercial_FisicaMoral_FisicaMoralId",
                        column: x => x.FisicaMoralId,
                        principalTable: "FisicaMoral",
                        principalColumn: "FisicaMoralId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReferenciaComercial_TipoRefComercial_TipoRefComercialId",
                        column: x => x.TipoRefComercialId,
                        principalTable: "TipoRefComercial",
                        principalColumn: "TipoRefComercialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferenciaPersonal",
                columns: table => new
                {
                    ReferenciaPersonalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RPNombres = table.Column<string>(nullable: true),
                    RPApePaterno = table.Column<string>(nullable: true),
                    RpApeMaterno = table.Column<string>(nullable: true),
                    RPTelefono = table.Column<string>(nullable: true),
                    TipoRefPersonalId = table.Column<int>(nullable: false),
                    FisicaMoralId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenciaPersonal", x => x.ReferenciaPersonalId);
                    table.ForeignKey(
                        name: "FK_ReferenciaPersonal_FisicaMoral_FisicaMoralId",
                        column: x => x.FisicaMoralId,
                        principalTable: "FisicaMoral",
                        principalColumn: "FisicaMoralId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReferenciaPersonal_TipoRefPersonal_TipoRefPersonalId",
                        column: x => x.TipoRefPersonalId,
                        principalTable: "TipoRefPersonal",
                        principalColumn: "TipoRefPersonalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poliza",
                columns: table => new
                {
                    PolizaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Creacion = table.Column<DateTime>(nullable: false),
                    PolizaEstatusId = table.Column<int>(nullable: false),
                    fechaEstatus = table.Column<DateTime>(nullable: false),
                    FiadorFId = table.Column<int>(nullable: true),
                    FiadorMId = table.Column<int>(nullable: true),
                    FisicaMoralId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliza", x => x.PolizaId);
                    table.ForeignKey(
                        name: "FK_Poliza_FiadorF_FiadorFId",
                        column: x => x.FiadorFId,
                        principalTable: "FiadorF",
                        principalColumn: "FiadorFId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Poliza_FiadorM_FiadorMId",
                        column: x => x.FiadorMId,
                        principalTable: "FiadorM",
                        principalColumn: "FiadorMId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Poliza_FisicaMoral_FisicaMoralId",
                        column: x => x.FisicaMoralId,
                        principalTable: "FisicaMoral",
                        principalColumn: "FisicaMoralId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Poliza_PolizaEstatus_PolizaEstatusId",
                        column: x => x.PolizaEstatusId,
                        principalTable: "PolizaEstatus",
                        principalColumn: "PolizaEstatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendario_FisicaMoralId",
                table: "Calendario",
                column: "FisicaMoralId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendario_UsuariosId",
                table: "Calendario",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoPlantilla_EstadosId",
                table: "DocumentoPlantilla",
                column: "EstadosId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_FisicaMoralId",
                table: "Documentos",
                column: "FisicaMoralId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_TipoDocumentoId",
                table: "Documentos",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_FiadorF_FisicaMoralId",
                table: "FiadorF",
                column: "FisicaMoralId");

            migrationBuilder.CreateIndex(
                name: "IX_FiadorM_FisicaMoralId",
                table: "FiadorM",
                column: "FisicaMoralId");

            migrationBuilder.CreateIndex(
                name: "IX_FisicaMoral_SolicitudId",
                table: "FisicaMoral",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuP_AreaId",
                table: "MenuP",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuP_MenuPPadreId",
                table: "MenuP",
                column: "MenuPPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonasOcupanInm_FisicaMoralId",
                table: "PersonasOcupanInm",
                column: "FisicaMoralId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonasOcupanInm_TipoParentescoId",
                table: "PersonasOcupanInm",
                column: "TipoParentescoId");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_FiadorFId",
                table: "Poliza",
                column: "FiadorFId");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_FiadorMId",
                table: "Poliza",
                column: "FiadorMId");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_FisicaMoralId",
                table: "Poliza",
                column: "FisicaMoralId");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_PolizaEstatusId",
                table: "Poliza",
                column: "PolizaEstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RefArrendamiento_FisicaMoralId",
                table: "RefArrendamiento",
                column: "FisicaMoralId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenciaComercial_FisicaMoralId",
                table: "ReferenciaComercial",
                column: "FisicaMoralId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenciaComercial_TipoRefComercialId",
                table: "ReferenciaComercial",
                column: "TipoRefComercialId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenciaPersonal_FisicaMoralId",
                table: "ReferenciaPersonal",
                column: "FisicaMoralId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenciaPersonal_TipoRefPersonalId",
                table: "ReferenciaPersonal",
                column: "TipoRefPersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_CentroCostosId",
                table: "Solicitud",
                column: "CentroCostosId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_EstadosId",
                table: "Solicitud",
                column: "EstadosId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_TipoInmobiliarioId",
                table: "Solicitud",
                column: "TipoInmobiliarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_AreaId",
                table: "Usuarios",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RepresentacionId",
                table: "Usuarios",
                column: "RepresentacionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSolicitud_SolicitudId",
                table: "UsuariosSolicitud",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSolicitud_UsuariosId",
                table: "UsuariosSolicitud",
                column: "UsuariosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calendario");

            migrationBuilder.DropTable(
                name: "DocumentoPlantilla");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "MenuP");

            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "PersonasOcupanInm");

            migrationBuilder.DropTable(
                name: "Poliza");

            migrationBuilder.DropTable(
                name: "RefArrendamiento");

            migrationBuilder.DropTable(
                name: "ReferenciaComercial");

            migrationBuilder.DropTable(
                name: "ReferenciaPersonal");

            migrationBuilder.DropTable(
                name: "UsuariosSolicitud");

            migrationBuilder.DropTable(
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "TipoParentesco");

            migrationBuilder.DropTable(
                name: "FiadorF");

            migrationBuilder.DropTable(
                name: "FiadorM");

            migrationBuilder.DropTable(
                name: "PolizaEstatus");

            migrationBuilder.DropTable(
                name: "TipoRefComercial");

            migrationBuilder.DropTable(
                name: "TipoRefPersonal");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "FisicaMoral");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Representacion");

            migrationBuilder.DropTable(
                name: "Solicitud");

            migrationBuilder.DropTable(
                name: "CentroCostos");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "TipoInmobiliario");
        }
    }
}
