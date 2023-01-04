using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PolizaJuridica.Data
{
    public partial class PolizaJuridicaDbContext : DbContext
    {
        public PolizaJuridicaDbContext()
        {
        }

        public PolizaJuridicaDbContext(DbContextOptions<PolizaJuridicaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Acuerdos> Acuerdos { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Arrendatario> Arrendatario { get; set; }
        public virtual DbSet<Calendario> Calendario { get; set; }
        public virtual DbSet<CategoriaEs> CategoriaEs { get; set; }
        public virtual DbSet<CentroCostos> CentroCostos { get; set; }
        public virtual DbSet<ClienteUsuario> ClienteUsuario { get; set; }
        public virtual DbSet<CuentasBancarias> CuentasBancarias { get; set; }
        public virtual DbSet<CuentasXcobrar> CuentasXcobrar { get; set; }
        public virtual DbSet<CuentasXpagar> CuentasXpagar { get; set; }
        public virtual DbSet<DetalleInvestigacion> DetalleInvestigacion { get; set; }
        public virtual DbSet<DetalleMovimiento> DetalleMovimiento { get; set; }
        public virtual DbSet<DetallePoliza> DetallePoliza { get; set; }
        public virtual DbSet<DocumentoPlantilla> DocumentoPlantilla { get; set; }
        public virtual DbSet<Documentos> Documentos { get; set; }
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistory { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<Eventos> Eventos { get; set; }
        public virtual DbSet<EventoUsuarios> EventoUsuarios { get; set; }
        public virtual DbSet<FiadorF> FiadorF { get; set; }
        public virtual DbSet<FiadorM> FiadorM { get; set; }
        public virtual DbSet<Firmas> Firmas { get; set; }
        public virtual DbSet<FisicaMoral> FisicaMoral { get; set; }
        public virtual DbSet<FlujoSolicitud> FlujoSolicitud { get; set; }
        public virtual DbSet<Investigacion> Investigacion { get; set; }
        public virtual DbSet<KeywordStructura> KeywordStructura { get; set; }
        public virtual DbSet<Listanegra> Listanegra { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<MenuP> MenuP { get; set; }
        public virtual DbSet<MovimientoMaestro> MovimientoMaestro { get; set; }
        public virtual DbSet<Movimientos> Movimientos { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<PersonasOcupanInm> PersonasOcupanInm { get; set; }
        public virtual DbSet<Poliza> Poliza { get; set; }
        public virtual DbSet<PolizasAnteriores> PolizasAnteriores { get; set; }
        public virtual DbSet<ProcesoSoluciones> ProcesoSoluciones { get; set; }
        public virtual DbSet<Prohemio> Prohemio { get; set; }
        public virtual DbSet<ProhemioC> ProhemioC { get; set; }
        public virtual DbSet<RefArrendamiento> RefArrendamiento { get; set; }
        public virtual DbSet<ReferenciaComercial> ReferenciaComercial { get; set; }
        public virtual DbSet<ReferenciaPersonal> ReferenciaPersonal { get; set; }
        public virtual DbSet<ReporteInvst> ReporteInvst { get; set; }
        public virtual DbSet<Representacion> Representacion { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<SolucionDetalle> SolucionDetalle { get; set; }
        public virtual DbSet<Soluciones> Soluciones { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoInmobiliario> TipoInmobiliario { get; set; }
        public virtual DbSet<TipoParentesco> TipoParentesco { get; set; }
        public virtual DbSet<TipoProceso> TipoProceso { get; set; }
        public virtual DbSet<TipoProcesoPo> TipoProcesoPo { get; set; }
        public virtual DbSet<TipoRefComercial> TipoRefComercial { get; set; }
        public virtual DbSet<TipoRefPersonal> TipoRefPersonal { get; set; }
        public virtual DbSet<UsuarioPoliza> UsuarioPoliza { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<UsuariosSolicitud> UsuariosSolicitud { get; set; }
        public virtual DbSet<UsuariosSoluciones> UsuariosSoluciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=pjbd.ct8o2hbw0cgi.us-east-1.rds.amazonaws.com;port=3306;database=BDPoliza;user=pjinstance;password=trhc_0253");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acuerdos>(entity =>
            {
                entity.HasIndex(e => e.DetalleInvestigacionId)
                    .HasName("Acuerdos_DetalleInvestigacion_Id_fk");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Acuerdo)
                    .HasColumnName("acuerdo")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.DetalleInvestigacionId).HasColumnType("int(11)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.Juicio)
                    .HasColumnName("juicio")
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.DetalleInvestigacion)
                    .WithMany(p => p.Acuerdos)
                    .HasForeignKey(d => d.DetalleInvestigacionId)
                    .HasConstraintName("Acuerdos_DetalleInvestigacion_Id_fk");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.AreaId).HasColumnType("int(11)");

                entity.Property(e => e.AreaDescripcion).HasColumnType("longtext");

                entity.Property(e => e.Dashboard).HasColumnType("varchar(80)");
            });

            modelBuilder.Entity<Arrendatario>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId);

                entity.Property(e => e.ArrendatarioId).HasColumnType("int(11)");

                entity.Property(e => e.ActaConstitutiva).HasColumnType("varchar(200)");

                entity.Property(e => e.AfianzadoRl)
                    .HasColumnName("AfianzadoRL")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.AfianzadoraRl)
                    .HasColumnName("AfianzadoraRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Antiguedad).HasColumnType("varchar(40)");

                entity.Property(e => e.ApeMaterno).HasColumnType("varchar(60)");

                entity.Property(e => e.ApePaterno).HasColumnType("varchar(60)");

                entity.Property(e => e.Celular).HasColumnType("varchar(150)");

                entity.Property(e => e.CodigoPostal).HasColumnType("varchar(8)");

                entity.Property(e => e.CodigoPostalTrabajo).HasColumnType("varchar(8)");

                entity.Property(e => e.Colonia).HasColumnType("varchar(200)");

                entity.Property(e => e.ColoniaRl)
                    .HasColumnName("ColoniaRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.ColoniaTrabajo).HasColumnType("varchar(200)");

                entity.Property(e => e.CondMigratoria).HasColumnType("varchar(200)");

                entity.Property(e => e.ConvenioEc)
                    .HasColumnName("ConvenioEC")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.DeleMuni).HasColumnType("varchar(200)");

                entity.Property(e => e.DelegMuniTrabajo).HasColumnType("varchar(200)");

                entity.Property(e => e.DelegacionMunicipio).HasColumnType("varchar(200)");

                entity.Property(e => e.Domicilio).HasColumnType("varchar(500)");

                entity.Property(e => e.DomicilioRepresentanteLegal).HasColumnType("varchar(500)");

                entity.Property(e => e.DomicilioTrabajo).HasColumnType("varchar(500)");

                entity.Property(e => e.Email).HasColumnType("varchar(200)");

                entity.Property(e => e.EmailJefe).HasColumnType("varchar(200)");

                entity.Property(e => e.EmailRl)
                    .HasColumnName("EmailRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Estado).HasColumnType("varchar(200)");

                entity.Property(e => e.EstadoCivil).HasColumnType("varchar(150)");

                entity.Property(e => e.EstadoRl)
                    .HasColumnName("EstadoRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.EstadoTrabajo).HasColumnType("varchar(200)");

                entity.Property(e => e.Factura).HasColumnType("bit(1)");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.GiroTrabajo).HasColumnType("varchar(200)");

                entity.Property(e => e.Horario).HasColumnType("varchar(200)");

                entity.Property(e => e.HorarioRl)
                    .HasColumnName("HorarioRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.IngresoMensual).HasColumnType("decimal(18,2)");

                entity.Property(e => e.IngresoMensualRl)
                    .HasColumnName("IngresoMensualRL")
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.JefeTrabajo).HasColumnType("varchar(200)");

                entity.Property(e => e.Nacionalidad).HasColumnType("varchar(150)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(40)");

                entity.Property(e => e.NumeroIdentificacion).HasColumnType("varchar(40)");

                entity.Property(e => e.PoderRepresentanteNo).HasColumnType("varchar(200)");

                entity.Property(e => e.Profesion).HasColumnType("varchar(200)");

                entity.Property(e => e.Puesto).HasColumnType("varchar(200)");

                entity.Property(e => e.PuestoJefe).HasColumnType("varchar(200)");

                entity.Property(e => e.RazonSocial).HasColumnType("varchar(300)");

                entity.Property(e => e.RequiereFacturaRl)
                    .HasColumnName("RequiereFacturaRL")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Rfc)
                    .HasColumnName("RFC")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.SindicadoRl)
                    .HasColumnName("SindicadoRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Telefono).HasColumnType("varchar(150)");

                entity.Property(e => e.TelefonoRl)
                    .HasColumnName("TelefonoRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.TelefonoTrabajo).HasColumnType("varchar(150)");

                entity.Property(e => e.TipoIdentificacion).HasColumnType("varchar(40)");

                entity.Property(e => e.TipoRegimenFiscal).HasColumnType("int(1)");

                entity.Property(e => e.Trabajo).HasColumnType("varchar(300)");

                entity.Property(e => e.WebTrabajo).HasColumnType("varchar(300)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.Arrendatario)
                    .HasForeignKey(d => d.FisicaMoralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Arrendatario_FisicaMoral_FisicaMoralId_fk");
            });

            modelBuilder.Entity<Calendario>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId);

                entity.HasIndex(e => e.UsuariosId);

                entity.Property(e => e.CalendarioId).HasColumnType("int(11)");

                entity.Property(e => e.CalendarioDescripcion).HasColumnType("varchar(500)");

                entity.Property(e => e.CalendarioEstatus).HasColumnType("varchar(150)");

                entity.Property(e => e.CalendarioUbicacion).HasColumnType("varchar(500)");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.UsuariosId).HasColumnType("int(11)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.Calendario)
                    .HasForeignKey(d => d.FisicaMoralId);

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Calendario)
                    .HasForeignKey(d => d.UsuariosId);
            });

            modelBuilder.Entity<CategoriaEs>(entity =>
            {
                entity.ToTable("CategoriaES");

                entity.HasIndex(e => e.CategoriaEspadreId)
                    .HasName("CategoriaES_CategoriaES_CategoriaESId_fk");

                entity.Property(e => e.CategoriaEsid)
                    .HasColumnName("CategoriaESId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoriaEspadreId)
                    .HasColumnName("CategoriaESPadreId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CuentasXpagar)
                    .HasColumnName("CuentasXPagar")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Poliza).HasColumnType("bit(1)");

                entity.Property(e => e.TipoEs)
                    .HasColumnName("TipoES")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CategoriaEspadre)
                    .WithMany(p => p.InverseCategoriaEspadre)
                    .HasForeignKey(d => d.CategoriaEspadreId)
                    .HasConstraintName("CategoriaES_CategoriaES_CategoriaESId_fk");
            });

            modelBuilder.Entity<CentroCostos>(entity =>
            {
                entity.Property(e => e.CentroCostosId).HasColumnType("int(11)");

                entity.Property(e => e.CentroCostosMonto).HasColumnType("decimal(18,2)");

                entity.Property(e => e.CentroCostosRentaFinal).HasColumnType("int(11)");

                entity.Property(e => e.CentroCostosRentaInicial).HasColumnType("int(11)");

                entity.Property(e => e.CentroCostosTipo).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<ClienteUsuario>(entity =>
            {
                entity.HasKey(e => e.ClienteUsuario1);

                entity.HasIndex(e => e.UsuariosId)
                    .HasName("ClienteUsuario_Usuarios_UsuariosId_fk");

                entity.Property(e => e.ClienteUsuario1)
                    .HasColumnName("ClienteUsuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApellidoMaterno).HasColumnType("varchar(40)");

                entity.Property(e => e.ApellidoPaterno).HasColumnType("varchar(40)");

                entity.Property(e => e.Celular).HasColumnType("varchar(40)");

                entity.Property(e => e.Correo).HasColumnType("varchar(100)");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fechaCreacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasColumnType("varchar(40)");

                entity.Property(e => e.UsuariosId).HasColumnType("int(11)");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.ClienteUsuario)
                    .HasForeignKey(d => d.UsuariosId)
                    .HasConstraintName("ClienteUsuario_Usuarios_UsuariosId_fk");
            });

            modelBuilder.Entity<CuentasBancarias>(entity =>
            {
                entity.HasKey(e => e.CuentaId);

                entity.Property(e => e.CuentaId).HasColumnType("int(11)");

                entity.Property(e => e.Banco).HasColumnType("varchar(20)");

                entity.Property(e => e.Clabe)
                    .HasColumnName("CLABE")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Estatus).HasColumnType("tinyint(1)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(40)");

                entity.Property(e => e.Saldo).HasColumnType("decimal(10,0)");
            });

            modelBuilder.Entity<CuentasXcobrar>(entity =>
            {
                entity.HasKey(e => e.Ccid);

                entity.ToTable("CuentasXCobrar");

                entity.HasIndex(e => e.Ccid)
                    .HasName("CuentasXCobrar_CCId_index");

                entity.HasIndex(e => e.PolizaId)
                    .HasName("CuentasXCobrar_PolizaId_index");

                entity.Property(e => e.Ccid)
                    .HasColumnName("CCId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Importe).HasColumnType("decimal(10,0)");

                entity.Property(e => e.ImporteIncr).HasColumnType("decimal(10,0)");

                entity.Property(e => e.Numeral).HasColumnType("int(11)");

                entity.Property(e => e.PolizaId).HasColumnType("int(11)");

                entity.HasOne(d => d.Poliza)
                    .WithMany(p => p.CuentasXcobrar)
                    .HasForeignKey(d => d.PolizaId)
                    .HasConstraintName("CuentasXCobrar_Poliza_PolizaId_fk");
            });

            modelBuilder.Entity<CuentasXpagar>(entity =>
            {
                entity.HasKey(e => e.Cpid);

                entity.ToTable("CuentasXPagar");

                entity.HasIndex(e => e.CategoriaId)
                    .HasName("CuentasXPagar_CategoriaES_CategoriaESId_fk");

                entity.HasIndex(e => e.PolizaId)
                    .HasName("CuentasXPagar_Poliza_PolizaId_fk");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("CuentasXPagar_Usuarios_UsuariosId_fk");

                entity.HasIndex(e => new { e.Cpid, e.CategoriaId, e.PolizaId })
                    .HasName("CuentasXPagar_CPId_CategoriaId_PolizaId_index");

                entity.Property(e => e.Cpid)
                    .HasColumnName("CPId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoriaId).HasColumnType("int(11)");

                entity.Property(e => e.Importe).HasColumnType("decimal(10,0)");

                entity.Property(e => e.PolizaId).HasColumnType("int(11)");

                entity.Property(e => e.UsuarioId).HasColumnType("int(11)");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.CuentasXpagarNavigation)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("CuentasXPagar_CategoriaES_CategoriaESId_fk");

                entity.HasOne(d => d.Poliza)
                    .WithMany(p => p.CuentasXpagar)
                    .HasForeignKey(d => d.PolizaId)
                    .HasConstraintName("CuentasXPagar_Poliza_PolizaId_fk");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.CuentasXpagar)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("CuentasXPagar_Usuarios_UsuariosId_fk");
            });

            modelBuilder.Entity<DetalleInvestigacion>(entity =>
            {
                entity.HasIndex(e => e.InvestigacionId)
                    .HasName("DetalleInvestigacion_Investigacion_InvestigacionId_fk");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Actor)
                    .HasColumnName("actor")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Circuitoid)
                    .HasColumnName("circuitoid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Demandado)
                    .HasColumnName("demandado")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Entidad).HasColumnType("varchar(200)");

                entity.Property(e => e.Expediente)
                    .HasColumnName("expediente")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.Fuente)
                    .HasColumnName("fuente")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Fuero)
                    .HasColumnName("fuero")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.InvestigacionId).HasColumnType("int(11)");

                entity.Property(e => e.Juzgado)
                    .HasColumnName("juzgado")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Juzgadoid)
                    .HasColumnName("juzgadoid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Tipo)
                    .HasColumnName("tipo")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Tribunal).HasColumnType("varchar(200)");

                entity.HasOne(d => d.Investigacion)
                    .WithMany(p => p.DetalleInvestigacion)
                    .HasForeignKey(d => d.InvestigacionId)
                    .HasConstraintName("DetalleInvestigacion_Investigacion_InvestigacionId_fk");
            });

            modelBuilder.Entity<DetalleMovimiento>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Importe).HasColumnType("decimal(10,0)");

                entity.Property(e => e.Observaciones).HasColumnType("varchar(100)");

                entity.Property(e => e.Origen).HasColumnType("varchar(30)");

                entity.Property(e => e.Referencias).HasColumnType("varchar(20)");

                entity.Property(e => e.Tipo).HasColumnType("int(11)");
            });

            modelBuilder.Entity<DetallePoliza>(entity =>
            {
                entity.HasIndex(e => e.CategoriaEsid)
                    .HasName("DetallePoliza_CategoriaES_CategoriaESId_fk");

                entity.HasIndex(e => e.PolizaId)
                    .HasName("DetallePoliza_Poliza_PolizaId_fk");

                entity.Property(e => e.DetallePolizaId).HasColumnType("int(11)");

                entity.Property(e => e.CategoriaEsid)
                    .HasColumnName("CategoriaESId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Importe).HasColumnType("decimal(10,0)");

                entity.Property(e => e.PolizaId).HasColumnType("int(11)");

                entity.HasOne(d => d.CategoriaEs)
                    .WithMany(p => p.DetallePoliza)
                    .HasForeignKey(d => d.CategoriaEsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetallePoliza_CategoriaES_CategoriaESId_fk");

                entity.HasOne(d => d.Poliza)
                    .WithMany(p => p.DetallePoliza)
                    .HasForeignKey(d => d.PolizaId)
                    .HasConstraintName("DetallePoliza_Poliza_PolizaId_fk");
            });

            modelBuilder.Entity<DocumentoPlantilla>(entity =>
            {
                entity.HasIndex(e => e.EstadosId);

                entity.Property(e => e.DocumentoPlantillaId).HasColumnType("int(11)");

                entity.Property(e => e.DocumentoCarta).HasColumnType("bit(1)");

                entity.Property(e => e.DocumentoFiador).HasColumnType("bit(1)");

                entity.Property(e => e.DocumentoInmueble).HasColumnType("bit(1)");

                entity.Property(e => e.DocumentoOriginal).HasColumnType("longtext");

                entity.Property(e => e.DocumentoPagare).HasColumnType("bit(1)");

                entity.Property(e => e.DocumentoPlantillaNombre).HasColumnType("varchar(500)");

                entity.Property(e => e.DocumentoPlantillaTipo).HasColumnType("varchar(200)");

                entity.Property(e => e.DocumentoPlantillaXml).HasColumnType("longtext");

                entity.Property(e => e.EstadosId).HasColumnType("int(11)");

                entity.HasOne(d => d.Estados)
                    .WithMany(p => p.DocumentoPlantilla)
                    .HasForeignKey(d => d.EstadosId);
            });

            modelBuilder.Entity<Documentos>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId);

                entity.HasIndex(e => e.TipoDocumentoId);

                entity.Property(e => e.DocumentosId).HasColumnType("int(11)");

                entity.Property(e => e.DocumentoDesc).HasColumnType("longtext");

                entity.Property(e => e.DocumentosImagen).HasColumnType("longtext");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.TipoDocumentoId).HasColumnType("int(11)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.Documentos)
                    .HasForeignKey(d => d.FisicaMoralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Documentos_FisicaMoral_FisicaMoralId_fk");

                entity.HasOne(d => d.TipoDocumento)
                    .WithMany(p => p.Documentos)
                    .HasForeignKey(d => d.TipoDocumentoId);
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId);

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasColumnType("varchar(150)");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<Estados>(entity =>
            {
                entity.Property(e => e.EstadosId).HasColumnType("int(11)");

                entity.Property(e => e.CodigoPostal).HasColumnType("varchar(50)");

                entity.Property(e => e.EstadoNombre).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Eventos>(entity =>
            {
                entity.Property(e => e.EventosId).HasColumnType("int(11)");

                entity.Property(e => e.Activo).HasColumnType("bit(1)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(500)");

                entity.Property(e => e.Imagen).HasColumnType("longtext");

                entity.Property(e => e.Lugar)
                    .HasColumnName("lugar")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.ReunionIde).HasColumnType("varchar(30)");

                entity.Property(e => e.ReunionPass).HasColumnType("varchar(30)");

                entity.Property(e => e.ReunionUr).HasColumnType("varchar(200)");

                entity.Property(e => e.Titulo).HasColumnType("varchar(300)");
            });

            modelBuilder.Entity<EventoUsuarios>(entity =>
            {
                entity.HasIndex(e => e.EstadoId)
                    .HasName("EventoUsuarios_Estados_EstadosId_fk");

                entity.HasIndex(e => e.EventosId)
                    .HasName("EventoUsuarios_Eventos_EventosId_fk");

                entity.HasIndex(e => e.RepresentanteId)
                    .HasName("EventoUsuarios_Usuarios_UsuariosId_fk");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ApellidoPaterno).HasColumnType("varchar(40)");

                entity.Property(e => e.Celular)
                    .HasColumnName("celular")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Correo).HasColumnType("varchar(40)");

                entity.Property(e => e.EstadoId).HasColumnType("int(11)");

                entity.Property(e => e.EventosId).HasColumnType("int(11)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(40)");

                entity.Property(e => e.RepresentanteId).HasColumnType("int(11)");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.EventoUsuarios)
                    .HasForeignKey(d => d.EstadoId)
                    .HasConstraintName("EventoUsuarios_Estados_EstadosId_fk");

                entity.HasOne(d => d.Eventos)
                    .WithMany(p => p.EventoUsuarios)
                    .HasForeignKey(d => d.EventosId)
                    .HasConstraintName("EventoUsuarios_Eventos_EventosId_fk");

                entity.HasOne(d => d.Representante)
                    .WithMany(p => p.EventoUsuarios)
                    .HasForeignKey(d => d.RepresentanteId)
                    .HasConstraintName("EventoUsuarios_Usuarios_UsuariosId_fk");
            });

            modelBuilder.Entity<FiadorF>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId);

                entity.HasIndex(e => e.TipoInmuebleId)
                    .HasName("FiadorF_TipoInmobiliario_TipoInmobiliarioId_fk");

                entity.Property(e => e.FiadorFid)
                    .HasColumnName("FiadorFId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DistritoJudicial).HasColumnType("varchar(200)");

                entity.Property(e => e.EscrituraNumero).HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFapeMaterno)
                    .HasColumnName("FiadorFApeMaterno")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFapeMaternoConyuge)
                    .HasColumnName("FiadorFApeMaternoConyuge")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFapePaterno)
                    .HasColumnName("FiadorFApePaterno")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFapePaternoConyuge)
                    .HasColumnName("FiadorFApePaternoConyuge")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFcelular)
                    .HasColumnName("FiadorFCelular")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFcodigoPostal)
                    .IsRequired()
                    .HasColumnName("FiadorFCodigoPostal")
                    .HasColumnType("varchar(8)");

                entity.Property(e => e.FiadorFcodigoPostalGarantia)
                    .IsRequired()
                    .HasColumnName("FiadorFCodigoPostalGarantia")
                    .HasColumnType("varchar(8)");

                entity.Property(e => e.FiadorFcolonia)
                    .HasColumnName("FiadorFColonia")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFcoloniaGarantia)
                    .HasColumnName("FiadorFColoniaGarantia")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFcondicionMigratoria)
                    .HasColumnName("FiadorFCondicionMigratoria")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFconvenioEc)
                    .HasColumnName("FiadorFConvenioEC")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFdelegacion)
                    .HasColumnName("FiadorFDelegacion")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFdelegacionGarantia)
                    .HasColumnName("FiadorFDelegacionGarantia")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFdomicilio)
                    .HasColumnName("FiadorFDomicilio")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.FiadorFdomicilioGarantia)
                    .HasColumnName("FiadorFDomicilioGarantia")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFemail)
                    .HasColumnName("FiadorFEmail")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFempresa)
                    .HasColumnName("FiadorFEmpresa")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFestado)
                    .HasColumnName("FiadorFEstado")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFestadoCivil)
                    .HasColumnName("FiadorFEstadoCivil")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFestadoGarantia)
                    .HasColumnName("FiadorFEstadoGarantia")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFnacionalidad)
                    .HasColumnName("FiadorFNacionalidad")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFnombres)
                    .HasColumnName("FiadorFNombres")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFnombresConyuge)
                    .HasColumnName("FiadorFNombresConyuge")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFparentesco)
                    .HasColumnName("FiadorFParentesco")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFprofesion)
                    .HasColumnName("FiadorFProfesion")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFtelefono)
                    .HasColumnName("FiadorFTelefono")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorFtelefonoEmpresa)
                    .HasColumnName("FiadorFTelefonoEmpresa")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.Licenciado).HasColumnType("varchar(200)");

                entity.Property(e => e.NumeroIdentificacion).HasColumnType("varchar(40)");

                entity.Property(e => e.NumeroNotaria).HasColumnType("varchar(200)");

                entity.Property(e => e.PartidaFecha).HasDefaultValueSql("'0001-01-01 00:00:00.000000'");

                entity.Property(e => e.PartidaLibro).HasColumnType("varchar(200)");

                entity.Property(e => e.PartidaNumero).HasColumnType("varchar(200)");

                entity.Property(e => e.PartidaSeccion).HasColumnType("varchar(200)");

                entity.Property(e => e.PartidaVolumen).HasColumnType("varchar(200)");

                entity.Property(e => e.TipoIdentificacion).HasColumnType("varchar(40)");

                entity.Property(e => e.TipoInmuebleId).HasColumnType("int(11)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.FiadorF)
                    .HasForeignKey(d => d.FisicaMoralId);

                entity.HasOne(d => d.TipoInmueble)
                    .WithMany(p => p.FiadorF)
                    .HasForeignKey(d => d.TipoInmuebleId)
                    .HasConstraintName("FiadorF_TipoInmobiliario_TipoInmobiliarioId_fk");
            });

            modelBuilder.Entity<FiadorM>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId);

                entity.Property(e => e.FiadorMid)
                    .HasColumnName("FiadorMId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FiaddorMrazonSocial)
                    .HasColumnName("FiaddorMRazonSocial")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.FiadorMapeMaternoRlegal)
                    .HasColumnName("FiadorMApeMaternoRLegal")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.FiadorMapePaternoRlegal)
                    .HasColumnName("FiadorMApePaternoRLegal")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.FiadorMcelularRlegal)
                    .HasColumnName("FiadorMCelularRLegal")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.FiadorMcodigoPostalGarantia)
                    .HasColumnName("FiadorMCodigoPostalGarantia")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.FiadorMcoliniaGarantia)
                    .HasColumnName("FiadorMColiniaGarantia")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMcoloniaEmpresa)
                    .HasColumnName("FiadorMColoniaEmpresa")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMcoloniaGarantia)
                    .HasColumnName("FiadorMColoniaGarantia")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMcondicionMigratoria)
                    .HasColumnName("FiadorMCondicionMigratoria")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FiadorMcpempresa)
                    .HasColumnName("FiadorMCPEmpresa")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.FiadorMcpgarantia)
                    .HasColumnName("FiadorMCPGarantia")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.FiadorMdeleEmpresa)
                    .HasColumnName("FiadorMDeleEmpresa")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FiadorMdelegacionGarantia)
                    .HasColumnName("FiadorMDelegacionGarantia")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMdistritoJudicial)
                    .HasColumnName("FiadorMDistritoJudicial")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMdomicilioEmpresa)
                    .HasColumnName("FiadorMDomicilioEmpresa")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMdomicilioGarantia)
                    .HasColumnName("FiadorMDomicilioGarantia")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.FiadorMemailRlegal)
                    .HasColumnName("FiadorMEmailRLegal")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.FiadorMestadoEmpresa)
                    .HasColumnName("FiadorMEstadoEmpresa")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FiadorMestadoGarantia)
                    .HasColumnName("FiadorMEstadoGarantia")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMfechaCons)
                    .HasColumnName("FiadorMFechaCons")
                    .HasColumnType("date");

                entity.Property(e => e.FiadorMfechaPartida)
                    .HasColumnName("FiadorMFechaPartida")
                    .HasColumnType("date");

                entity.Property(e => e.FiadorMfechaPoder)
                    .HasColumnName("FiadorMFechaPoder")
                    .HasColumnType("date");

                entity.Property(e => e.FiadorMfechaRpp)
                    .HasColumnName("FiadorMFechaRpp")
                    .HasColumnType("date");

                entity.Property(e => e.FiadorMgiro)
                    .HasColumnName("FiadorMGiro")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.FiadorMlibro)
                    .HasColumnName("FiadorMLibro")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMlicenciadoCons)
                    .HasColumnName("FiadorMLicenciadoCons")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FiadorMlicenciadoNotaria)
                    .HasColumnName("FiadorMLicenciadoNotaria")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMlicenciadoPoder)
                    .HasColumnName("FiadorMLicenciadoPoder")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FiadorMmactaNo)
                    .HasColumnName("FiadorMMActaNo")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMnacionalidadRlegal)
                    .HasColumnName("FiadorMNacionalidadRLegal")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.FiadorMnombreEscrituraGarantia)
                    .HasColumnName("FiadorMNombreEscrituraGarantia")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMnombresRlegal)
                    .HasColumnName("FiadorMNombresRLegal")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.FiadorMnumCons)
                    .HasColumnName("FiadorMNumCons")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FiadorMnumEscPoder)
                    .HasColumnName("FiadorMNumEscPoder")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FiadorMnumNotaCons)
                    .HasColumnName("FiadorMNumNotaCons")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FiadorMnumNotaria)
                    .HasColumnName("FiadorMNumNotaria")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMnumPartida)
                    .HasColumnName("FiadorMNumPartida")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMnumRpp)
                    .HasColumnName("FiadorMNumRpp")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FiadorMnumeroNotaPoder)
                    .HasColumnName("FiadorMNumeroNotaPoder")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FiadorMpoderRepNo)
                    .HasColumnName("FiadorMPoderRepNo")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMpuestoRlegal)
                    .HasColumnName("FiadorMPuestoRLegal")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.FiadorMrfc)
                    .HasColumnName("FiadorMRFC")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.FiadorMseccion)
                    .HasColumnName("FiadorMSeccion")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMtelefono)
                    .HasColumnName("FiadorMTelefono")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.FiadorMtelefonoRlegat)
                    .HasColumnName("FiadorMTelefonoRLegat")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.FiadorMvolumen)
                    .HasColumnName("FiadorMVolumen")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FiadorMweb)
                    .HasColumnName("FiadorMWeb")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.NumeroIdentificacion).HasColumnType("varchar(40)");

                entity.Property(e => e.TipoIdentificacion).HasColumnType("varchar(40)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.FiadorM)
                    .HasForeignKey(d => d.FisicaMoralId);
            });

            modelBuilder.Entity<Firmas>(entity =>
            {
                entity.HasIndex(e => e.CreadaPorId)
                    .HasName("Firmas_Usuarios_UsuariosId_fk");

                entity.HasIndex(e => e.FirmanteId)
                    .HasName("Firmas_Usuarios_UsuariosId_fk_2");

                entity.HasIndex(e => e.PolizaId)
                    .HasName("Firmas_PolizaId_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CreadaPorId).HasColumnType("int(11)");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaFirma).HasColumnType("datetime");

                entity.Property(e => e.FirmanteId).HasColumnType("int(11)");

                entity.Property(e => e.Lugar).HasColumnType("varchar(100)");

                entity.Property(e => e.PolizaId).HasColumnType("int(11)");

                entity.HasOne(d => d.CreadaPor)
                    .WithMany(p => p.FirmasCreadaPor)
                    .HasForeignKey(d => d.CreadaPorId)
                    .HasConstraintName("Firmas_Usuarios_UsuariosId_fk");

                entity.HasOne(d => d.Firmante)
                    .WithMany(p => p.FirmasFirmante)
                    .HasForeignKey(d => d.FirmanteId)
                    .HasConstraintName("Firmas_Usuarios_UsuariosId_fk_2");

                entity.HasOne(d => d.Poliza)
                    .WithOne(p => p.Firmas)
                    .HasForeignKey<Firmas>(d => d.PolizaId)
                    .HasConstraintName("Firmas_Poliza_PolizaId_fk");
            });

            modelBuilder.Entity<FisicaMoral>(entity =>
            {
                entity.HasIndex(e => e.SolicitudId);

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.AfianzadoRl)
                    .HasColumnName("AfianzadoRL")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.AfianzadoraRl)
                    .HasColumnName("AfianzadoraRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.ColoniaRl)
                    .HasColumnName("ColoniaRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.DeleMuni).HasColumnType("varchar(200)");

                entity.Property(e => e.DomicilioRepresentanteLegal).HasColumnType("varchar(500)");

                entity.Property(e => e.EmailRl)
                    .HasColumnName("EmailRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.EscrituraNumero).HasColumnType("varchar(200)");

                entity.Property(e => e.EstadoRl)
                    .HasColumnName("EstadoRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.FechaConstitutiva).HasColumnType("date");

                entity.Property(e => e.FechaEmitePoder).HasColumnType("date");

                entity.Property(e => e.FechaRppcons)
                    .HasColumnName("FechaRPPCons")
                    .HasColumnType("date");

                entity.Property(e => e.HorarioRl)
                    .HasColumnName("HorarioRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.IngresoMensualRl)
                    .HasColumnName("IngresoMensualRL")
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Licenciado).HasColumnType("varchar(200)");

                entity.Property(e => e.NumEscPoder).HasColumnType("varchar(200)");

                entity.Property(e => e.NumNotaria).HasColumnType("varchar(200)");

                entity.Property(e => e.NumRppcons)
                    .HasColumnName("NumRPPCons")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.NumeroIdentificacion).HasColumnType("varchar(40)");

                entity.Property(e => e.NumeroNotaria).HasColumnType("varchar(200)");

                entity.Property(e => e.RequiereFacturaRl)
                    .HasColumnName("RequiereFacturaRL")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.SfisicaAntiguedad)
                    .HasColumnName("SFisicaAntiguedad")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.SfisicaApeMat)
                    .HasColumnName("SFisicaApeMat")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaApePat)
                    .HasColumnName("SFisicaApePat")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaCelular)
                    .HasColumnName("SFisicaCelular")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.SfisicaCodigoPostal)
                    .HasColumnName("SFisicaCodigoPostal")
                    .HasColumnType("varchar(8)");

                entity.Property(e => e.SfisicaCodigoPostalTrabajo)
                    .HasColumnName("SFisicaCodigoPostalTrabajo")
                    .HasColumnType("varchar(8)");

                entity.Property(e => e.SfisicaColonia)
                    .HasColumnName("SFisicaColonia")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaColoniaTrabajo)
                    .HasColumnName("SFisicaColoniaTrabajo")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaCondMigratoria)
                    .HasColumnName("SFisicaCondMigratoria")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaConvenioEc)
                    .HasColumnName("SFisicaConvenioEC")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.SfisicaDelegMuniTrabajo)
                    .HasColumnName("SFisicaDelegMuniTrabajo")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaDelegacionMunicipio)
                    .HasColumnName("SFisicaDelegacionMunicipio")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaDomicilio)
                    .HasColumnName("SFisicaDomicilio")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.SfisicaDomicilioTrabajo)
                    .HasColumnName("SFisicaDomicilioTrabajo")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.SfisicaEmail)
                    .HasColumnName("SFisicaEmail")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaEmailJefe)
                    .HasColumnName("SFisicaEmailJefe")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaEstado)
                    .HasColumnName("SFisicaEstado")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaEstadoCivil)
                    .HasColumnName("SFisicaEstadoCivil")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.SfisicaEstadoTrabajo)
                    .HasColumnName("SFisicaEstadoTrabajo")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaFactura)
                    .HasColumnName("SFisicaFactura")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.SfisicaGiroTrabajo)
                    .HasColumnName("SFisicaGiroTrabajo")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaHorario)
                    .HasColumnName("SFisicaHorario")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaIngresoMensual)
                    .HasColumnName("SFisicaIngresoMensual")
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.SfisicaJefeTrabajo)
                    .HasColumnName("SFisicaJefeTrabajo")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaNacionallidad)
                    .HasColumnName("SFisicaNacionallidad")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.SfisicaNombre)
                    .HasColumnName("SFisicaNombre")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaProfesion)
                    .HasColumnName("SFisicaProfesion")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaPuesto)
                    .HasColumnName("SFisicaPuesto")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaPuestoJefe)
                    .HasColumnName("SFisicaPuestoJefe")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SfisicaRazonSocial)
                    .HasColumnName("SFisicaRazonSocial")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.SfisicaRfc)
                    .HasColumnName("SFisicaRFC")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.SfisicaTelefono)
                    .HasColumnName("SFisicaTelefono")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.SfisicaTelefonoTrabajo)
                    .HasColumnName("SFisicaTelefonoTrabajo")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.SfisicaTrabajo)
                    .HasColumnName("SFisicaTrabajo")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.SfisicaWebTrabajo)
                    .HasColumnName("SFisicaWebTrabajo")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.SindicadoRl)
                    .HasColumnName("SindicadoRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudId).HasColumnType("int(11)");

                entity.Property(e => e.TelefonoRl)
                    .HasColumnName("TelefonoRL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.TipoIdentificacion).HasColumnType("varchar(40)");

                entity.Property(e => e.TitularNotaPoder).HasColumnType("varchar(200)");

                entity.HasOne(d => d.Solicitud)
                    .WithMany(p => p.FisicaMoral)
                    .HasForeignKey(d => d.SolicitudId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FisicaMoral_Solicitud_SolicitudId_fk");
            });

            modelBuilder.Entity<FlujoSolicitud>(entity =>
            {
                entity.HasIndex(e => e.PersonaId)
                    .HasName("FlujoSolicitud_Usuarios_UsuariosId_fk");

                entity.HasIndex(e => e.SolicitudId)
                    .HasName("FlujoSolicitud_Solicitud_SolicitudId_fk");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

                entity.Property(e => e.PersonaId).HasColumnType("int(11)");

                entity.Property(e => e.SolicitudId).HasColumnType("int(11)");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.FlujoSolicitud)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FlujoSolicitud_Usuarios_UsuariosId_fk");

                entity.HasOne(d => d.Solicitud)
                    .WithMany(p => p.FlujoSolicitud)
                    .HasForeignKey(d => d.SolicitudId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FlujoSolicitud_Solicitud_SolicitudId_fk");
            });

            modelBuilder.Entity<Investigacion>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId)
                    .HasName("Investigacion_FisicaMoral_FisicaMoralId_fk");

                entity.Property(e => e.InvestigacionId).HasColumnType("int(11)");

                entity.Property(e => e.Cantidad).HasColumnType("int(11)");

                entity.Property(e => e.Criterio).HasColumnType("varchar(200)");

                entity.Property(e => e.Detalle).HasColumnType("int(11)");

                entity.Property(e => e.Entidad).HasColumnType("varchar(50)");

                entity.Property(e => e.FechaFinal).HasColumnType("date");

                entity.Property(e => e.FechaInicial).HasColumnType("date");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(250)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.Investigacion)
                    .HasForeignKey(d => d.FisicaMoralId)
                    .HasConstraintName("Investigacion_FisicaMoral_FisicaMoralId_fk");
            });

            modelBuilder.Entity<KeywordStructura>(entity =>
            {
                entity.HasIndex(e => e.TipoInmobiliarioId)
                    .HasName("KeywordStructura___fk_TipoInmueble");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Comentarios).HasColumnType("varchar(500)");

                entity.Property(e => e.Estructura).HasColumnType("varchar(5000)");

                entity.Property(e => e.Keyword).HasColumnType("varchar(40)");

                entity.Property(e => e.Orden).HasColumnType("int(11)");

                entity.Property(e => e.TipoInmobiliarioId).HasColumnType("int(11)");

                entity.HasOne(d => d.TipoInmobiliario)
                    .WithMany(p => p.KeywordStructura)
                    .HasForeignKey(d => d.TipoInmobiliarioId)
                    .HasConstraintName("KeywordStructura___fk_TipoInmueble");
            });

            modelBuilder.Entity<Listanegra>(entity =>
            {
                entity.HasIndex(e => e.ListaNegraId)
                    .HasName("Listanegra_ListaNegraId_uindex")
                    .IsUnique();

                entity.Property(e => e.ListaNegraId).HasColumnType("int(11)");

                entity.Property(e => e.ApellidoMaterno).HasColumnType("varchar(40)");

                entity.Property(e => e.ApellidoPaterno).HasColumnType("varchar(40)");

                entity.Property(e => e.Estatus).HasColumnType("tinyint(1)");

                entity.Property(e => e.Nombres).HasColumnType("varchar(40)");

                entity.Property(e => e.Observaciones).HasColumnType("varchar(500)");

                entity.Property(e => e.RazonSocial).HasColumnType("varchar(150)");

                entity.Property(e => e.Rfc)
                    .HasColumnName("RFC")
                    .HasColumnType("varchar(15)");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.LogId).HasColumnType("int(11)");

                entity.Property(e => e.LogObjetoIn).HasColumnType("longtext");

                entity.Property(e => e.LogObjetoOut).HasColumnType("longtext");

                entity.Property(e => e.LogPantalla).HasColumnType("longtext");

                entity.Property(e => e.LogProceso).HasColumnType("longtext");
            });

            modelBuilder.Entity<MenuP>(entity =>
            {
                entity.HasIndex(e => e.AreaId);

                entity.HasIndex(e => e.MenuPpadreId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AreaId).HasColumnType("int(11)");

                entity.Property(e => e.Controlador).HasColumnType("longtext");

                entity.Property(e => e.MenuPpadreId)
                    .HasColumnName("MenuPPadreId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre).HasColumnType("longtext");

                entity.Property(e => e.Pantalla).HasColumnType("longtext");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.MenuP)
                    .HasForeignKey(d => d.AreaId);

                entity.HasOne(d => d.MenuPpadre)
                    .WithMany(p => p.InverseMenuPpadre)
                    .HasForeignKey(d => d.MenuPpadreId);
            });

            modelBuilder.Entity<MovimientoMaestro>(entity =>
            {
                entity.HasIndex(e => e.CuentaId)
                    .HasName("MovimientoMaestro_CuentasBancarias_CuentasBancarias_fk");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("MovimientoMaestro_Usuarios_UsuariosId_fk");

                entity.HasIndex(e => new { e.Id, e.CuentaId })
                    .HasName("MovimientoMaestro_Id_CuentaId_index");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CuentaId).HasColumnType("int(11)");

                entity.Property(e => e.Entrada).HasColumnType("decimal(10,0)");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Salida).HasColumnType("decimal(10,0)");

                entity.Property(e => e.UsuarioId).HasColumnType("int(11)");

                entity.HasOne(d => d.Cuenta)
                    .WithMany(p => p.MovimientoMaestro)
                    .HasForeignKey(d => d.CuentaId)
                    .HasConstraintName("MovimientoMaestro_CuentasBancarias_CuentasBancarias_fk");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.MovimientoMaestro)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("MovimientoMaestro_Usuarios_UsuariosId_fk");
            });

            modelBuilder.Entity<Movimientos>(entity =>
            {
                entity.HasIndex(e => e.Ccid)
                    .HasName("Movimientos_CuentasXCobrar_CCId_fk");

                entity.HasIndex(e => e.Cpid)
                    .HasName("Movimientos_CuentasXPagar_CPId_fk");

                entity.HasIndex(e => e.DetallePolizaId)
                    .HasName("Movimientos_DetallePoliza_DetallePolizaId_fk");

                entity.HasIndex(e => e.Dmid)
                    .HasName("Movimientos_DetalleMovimiento_Id_fk");

                entity.HasIndex(e => e.IdPadre)
                    .HasName("Movimientos_Movimientos_Id_fk");

                entity.HasIndex(e => e.Mmid)
                    .HasName("Movimientos_MovimientoMaestro_Id_fk");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Ccid)
                    .HasColumnName("CCId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cpid)
                    .HasColumnName("CPId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DetallePolizaId).HasColumnType("int(11)");

                entity.Property(e => e.Dmid)
                    .HasColumnName("DMId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdPadre).HasColumnType("int(11)");

                entity.Property(e => e.Mmid)
                    .HasColumnName("MMId")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Cc)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.Ccid)
                    .HasConstraintName("Movimientos_CuentasXCobrar_CCId_fk");

                entity.HasOne(d => d.Cp)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.Cpid)
                    .HasConstraintName("Movimientos_CuentasXPagar_CPId_fk");

                entity.HasOne(d => d.DetallePoliza)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.DetallePolizaId)
                    .HasConstraintName("Movimientos_DetallePoliza_DetallePolizaId_fk");

                entity.HasOne(d => d.Dm)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.Dmid)
                    .HasConstraintName("Movimientos_DetalleMovimiento_Id_fk");

                entity.HasOne(d => d.IdPadreNavigation)
                    .WithMany(p => p.InverseIdPadreNavigation)
                    .HasForeignKey(d => d.IdPadre)
                    .HasConstraintName("Movimientos_Movimientos_Id_fk");

                entity.HasOne(d => d.Mm)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.Mmid)
                    .HasConstraintName("Movimientos_MovimientoMaestro_Id_fk");
            });

            modelBuilder.Entity<Parametros>(entity =>
            {
                entity.Property(e => e.ParametrosId).HasColumnType("int(11)");

                entity.Property(e => e.ParametroNombre).HasColumnType("longtext");

                entity.Property(e => e.ParametroValor).HasColumnType("longtext");

                entity.Property(e => e.ParametroValor1).HasColumnType("longtext");

                entity.Property(e => e.ParametroValor2).HasColumnType("longtext");

                entity.Property(e => e.ParametroValor3).HasColumnType("longtext");

                entity.Property(e => e.ParametroValor4).HasColumnType("longtext");

                entity.Property(e => e.ParametroValorNumerico).HasColumnType("longtext");
            });

            modelBuilder.Entity<PersonasOcupanInm>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId);

                entity.HasIndex(e => e.TipoParentescoId);

                entity.Property(e => e.PersonasOcupanInmId).HasColumnType("int(11)");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.PersonasOcupanInmNombre).HasColumnType("varchar(200)");

                entity.Property(e => e.TipoParentescoId).HasColumnType("int(11)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.PersonasOcupanInm)
                    .HasForeignKey(d => d.FisicaMoralId);

                entity.HasOne(d => d.TipoParentesco)
                    .WithMany(p => p.PersonasOcupanInm)
                    .HasForeignKey(d => d.TipoParentescoId);
            });

            modelBuilder.Entity<Poliza>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId);

                entity.Property(e => e.PolizaId).HasColumnType("int(11)");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.PolizaAnterior).HasColumnType("varchar(40)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.Poliza)
                    .HasForeignKey(d => d.FisicaMoralId)
                    .HasConstraintName("Poliza_FisicaMoral_FisicaMoralId_fk");
            });

            modelBuilder.Entity<PolizasAnteriores>(entity =>
            {
                entity.Property(e => e.PolizasAnterioresId).HasColumnType("int(11)");

                entity.Property(e => e.AgenteInmobiliario)
                    .HasColumnName("Agente Inmobiliario")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CostoPóliza)
                    .HasColumnName("Costo Póliza")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DeFecha)
                    .HasColumnName("De Fecha")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DiaDePagoRenta)
                    .HasColumnName("Dia de pago renta")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DireccionDelFiador)
                    .HasColumnName("Direccion del Fiador")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DirecciónAval)
                    .HasColumnName("Dirección aval")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DirecciónInmue)
                    .HasColumnName("dirección inmue")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DirecciónInq)
                    .HasColumnName("dirección inq")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DirecciónProp)
                    .HasColumnName("dirección prop")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EjecutivoPj)
                    .HasColumnName("Ejecutivo PJ")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EmailAgenteInmob)
                    .HasColumnName("email agente inmob")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EmailFiador)
                    .HasColumnName("email fiador")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EmailInquilino)
                    .HasColumnName("email inquilino")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EmailProp)
                    .HasColumnName("email prop")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EscrituraNo)
                    .HasColumnName("Escritura No")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FechaFirmaContrato)
                    .HasColumnName("Fecha Firma contrato")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FechaRpp)
                    .HasColumnName("Fecha RPP")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IdentificadoComo)
                    .HasColumnName("Identificado como")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IfeAval)
                    .HasColumnName("ife aval")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IfeInq)
                    .HasColumnName("ife inq")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IfePro)
                    .HasColumnName("ife pro")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ImporteRenta)
                    .HasColumnName("Importe renta")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Libro).HasColumnType("varchar(255)");

                entity.Property(e => e.MunDel)
                    .HasColumnName("Mun Del")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Municipio).HasColumnType("varchar(255)");

                entity.Property(e => e.NomNotario)
                    .HasColumnName("Nom Notario")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NombreAval)
                    .HasColumnName("Nombre aval")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NombreDelAgente)
                    .HasColumnName("Nombre del agente")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NombreFiador)
                    .HasColumnName("Nombre Fiador")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NombreInmob)
                    .HasColumnName("Nombre Inmob")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NombreInq)
                    .HasColumnName("Nombre Inq")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NombreProp)
                    .HasColumnName("Nombre Prop")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NumNotario)
                    .HasColumnName("Num Notario")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NúmeroPóliza)
                    .HasColumnName("Número Póliza")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OficinaPj)
                    .HasColumnName("OFICINA PJ")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Partida).HasColumnType("varchar(255)");

                entity.Property(e => e.PrimerDiaVencimiento)
                    .HasColumnName("primer dia vencimiento")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Seccion).HasColumnType("varchar(255)");

                entity.Property(e => e.TelInmobiliaria)
                    .HasColumnName("Tel  inmobiliaria")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TelefonoFiador)
                    .HasColumnName("Telefono Fiador")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TeléfonoAval)
                    .HasColumnName("Teléfono aval")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TeléfonoInq)
                    .HasColumnName("Teléfono inq")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TeléfonoProp)
                    .HasColumnName("Teléfono prop")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UsoInmueb)
                    .HasColumnName("Uso inmueb")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.VigenciaContrato)
                    .HasColumnName("Vigencia Contrato")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.VigenciaPóliza)
                    .HasColumnName("Vigencia Póliza")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Volumen).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ProcesoSoluciones>(entity =>
            {
                entity.Property(e => e.ProcesoSolucionesId).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<Prohemio>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Texto).HasColumnType("varchar(2000)");
            });

            modelBuilder.Entity<ProhemioC>(entity =>
            {
                entity.HasIndex(e => e.ProhemioId)
                    .HasName("ProhemioC_Prohemio_Id_fk");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Cantidad).HasColumnType("int(11)");

                entity.Property(e => e.PersonalidadJuridica)
                    .HasColumnName("Personalidad Juridica")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProhemioId).HasColumnType("int(11)");

                entity.Property(e => e.Tipo).HasColumnType("int(11)");

                entity.HasOne(d => d.Prohemio)
                    .WithMany(p => p.ProhemioC)
                    .HasForeignKey(d => d.ProhemioId)
                    .HasConstraintName("ProhemioC_Prohemio_Id_fk");
            });

            modelBuilder.Entity<RefArrendamiento>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId);

                entity.Property(e => e.RefArrendamientoId).HasColumnType("int(11)");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.RefArrenApeMaterno).HasColumnType("varchar(200)");

                entity.Property(e => e.RefArrenApePaterno).HasColumnType("varchar(200)");

                entity.Property(e => e.RefArrenDomicilio).HasColumnType("varchar(500)");

                entity.Property(e => e.RefArrenMonto).HasColumnType("decimal(18,2)");

                entity.Property(e => e.RefArrenMotivoCambio).HasColumnType("varchar(500)");

                entity.Property(e => e.RefArrenNombres).HasColumnType("varchar(200)");

                entity.Property(e => e.RefArrenTelefono).HasColumnType("varchar(200)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.RefArrendamiento)
                    .HasForeignKey(d => d.FisicaMoralId);
            });

            modelBuilder.Entity<ReferenciaComercial>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId);

                entity.HasIndex(e => e.TipoRefComercialId);

                entity.Property(e => e.ReferenciaComercialId).HasColumnType("int(11)");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.Rcdetalle)
                    .HasColumnName("RCDetalle")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Rcrepresenta)
                    .HasColumnName("RCREpresenta")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.TipoRefComercialId).HasColumnType("int(11)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.ReferenciaComercial)
                    .HasForeignKey(d => d.FisicaMoralId);

                entity.HasOne(d => d.TipoRefComercial)
                    .WithMany(p => p.ReferenciaComercial)
                    .HasForeignKey(d => d.TipoRefComercialId);
            });

            modelBuilder.Entity<ReferenciaPersonal>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId);

                entity.HasIndex(e => e.TipoRefPersonalId);

                entity.Property(e => e.ReferenciaPersonalId).HasColumnType("int(11)");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.RpApeMaterno).HasColumnType("varchar(200)");

                entity.Property(e => e.RpapePaterno)
                    .HasColumnName("RPApePaterno")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Rpnombres)
                    .HasColumnName("RPNombres")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Rptelefono)
                    .HasColumnName("RPTelefono")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TipoRefPersonalId).HasColumnType("int(11)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.ReferenciaPersonal)
                    .HasForeignKey(d => d.FisicaMoralId);

                entity.HasOne(d => d.TipoRefPersonal)
                    .WithMany(p => p.ReferenciaPersonal)
                    .HasForeignKey(d => d.TipoRefPersonalId);
            });

            modelBuilder.Entity<ReporteInvst>(entity =>
            {
                entity.HasIndex(e => e.FisicaMoralId)
                    .HasName("ReporteInvst_FisicaMoral_FisicaMoralId_fk");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.FisicaMoralId).HasColumnType("int(11)");

                entity.Property(e => e.Texto1).HasColumnType("varchar(5000)");

                entity.Property(e => e.Texto2).HasColumnType("varchar(5000)");

                entity.HasOne(d => d.FisicaMoral)
                    .WithMany(p => p.ReporteInvst)
                    .HasForeignKey(d => d.FisicaMoralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ReporteInvst_FisicaMoral_FisicaMoralId_fk");
            });

            modelBuilder.Entity<Representacion>(entity =>
            {
                entity.Property(e => e.RepresentacionId).HasColumnType("int(11)");

                entity.Property(e => e.Activa).HasColumnType("bit(1)");

                entity.Property(e => e.Direccion).HasColumnType("varchar(500)");

                entity.Property(e => e.Foranea).HasColumnType("bit(1)");

                entity.Property(e => e.Latitud).HasColumnType("varchar(40)");

                entity.Property(e => e.Longitud).HasColumnType("varchar(40)");

                entity.Property(e => e.OficinaEmisora).HasColumnType("varchar(40)");

                entity.Property(e => e.Porcentaje).HasColumnType("decimal(10,0)");

                entity.Property(e => e.PorcentajeAsesor).HasColumnType("decimal(10,0)");

                entity.Property(e => e.PorcentajeEjecutivo).HasColumnType("decimal(10,0)");

                entity.Property(e => e.RepresentacionNombre).HasColumnType("varchar(300)");

                entity.Property(e => e.TelefonoOficina).HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.HasIndex(e => e.Asesorid)
                    .HasName("Solicitud_Usuarios_UsuariosId_fk_2");

                entity.HasIndex(e => e.CentroCostosId);

                entity.HasIndex(e => e.Creadorid)
                    .HasName("Solicitud_Usuarios_UsuariosId_fk_3");

                entity.HasIndex(e => e.EstadosId);

                entity.HasIndex(e => e.Representanteid)
                    .HasName("Solicitud_Usuarios_UsuariosId_fk");

                entity.HasIndex(e => e.SolicitudId)
                    .HasName("Solicitud_SolicitudId_index");

                entity.HasIndex(e => e.TipoInmobiliarioId);

                entity.Property(e => e.AlcaldiaMunicipioActual).HasColumnType("varchar(200)");

                entity.Property(e => e.AlcaldiaMunicipioArrendar).HasColumnType("varchar(200)");

                entity.Property(e => e.ApeMat1).HasColumnType("varchar(80)");

                entity.Property(e => e.ApeMat2).HasColumnType("varchar(80)");

                entity.Property(e => e.ApeMat3).HasColumnType("varchar(80)");

                entity.Property(e => e.ApePat1).HasColumnType("varchar(80)");

                entity.Property(e => e.ApePat2).HasColumnType("varchar(80)");

                entity.Property(e => e.ApePat3).HasColumnType("varchar(80)");

                entity.Property(e => e.ArrendatarioApeMat).HasColumnType("varchar(200)");

                entity.Property(e => e.ArrendatarioApePat).HasColumnType("varchar(200)");

                entity.Property(e => e.ArrendatarioCorreo).HasColumnType("varchar(200)");

                entity.Property(e => e.ArrendatarioNombre).HasColumnType("varchar(200)");

                entity.Property(e => e.ArrendatarioTelefono).HasColumnType("varchar(100)");

                entity.Property(e => e.CodigoPostalActual).HasColumnType("varchar(8)");

                entity.Property(e => e.CodigoPostalArrendar).HasColumnType("varchar(8)");

                entity.Property(e => e.ColoniaActual).HasColumnType("varchar(200)");

                entity.Property(e => e.ColoniaArrendar).HasColumnType("varchar(200)");

                entity.Property(e => e.CostoPoliza).HasColumnType("decimal(18,2)");

                entity.Property(e => e.EsAmueblado)
                    .HasColumnName("esAmueblado")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.EscrituraNumero).HasColumnType("varchar(200)");

                entity.Property(e => e.EstadoActual).HasColumnType("varchar(200)");

                entity.Property(e => e.FechaConstitutiva).HasColumnType("date");

                entity.Property(e => e.FechaEmitePoder).HasColumnType("date");

                entity.Property(e => e.FechaRppcons)
                    .HasColumnName("FechaRPPCons")
                    .HasColumnType("date");

                entity.Property(e => e.Inmobiliaria).HasColumnType("varchar(300)");

                entity.Property(e => e.IsRenovacion).HasColumnType("bit(1)");

                entity.Property(e => e.Jurisdiccion)
                    .HasColumnName("jurisdiccion")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Licenciado).HasColumnType("varchar(200)");

                entity.Property(e => e.Nombre1).HasColumnType("varchar(80)");

                entity.Property(e => e.Nombre2).HasColumnType("varchar(80)");

                entity.Property(e => e.Nombre3).HasColumnType("varchar(80)");

                entity.Property(e => e.NumEscPoder).HasColumnType("varchar(200)");

                entity.Property(e => e.NumIdent1).HasColumnType("varchar(40)");

                entity.Property(e => e.NumIdent2).HasColumnType("varchar(40)");

                entity.Property(e => e.NumIdent3).HasColumnType("varchar(40)");

                entity.Property(e => e.NumNotaria).HasColumnType("varchar(200)");

                entity.Property(e => e.NumRppcons)
                    .HasColumnName("NumRPPCons")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.NumeroIdentificacion).HasColumnType("varchar(40)");

                entity.Property(e => e.NumeroNotaria).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudAdmiInmueble).HasColumnType("bit(1)");

                entity.Property(e => e.SolicitudApeMaternoLegal).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudApeMaternoProp).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudApePaternoLegal).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudApePaternoProp).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudBanco).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudCartaEntrega).HasColumnType("bit(1)");

                entity.Property(e => e.SolicitudCelular).HasColumnType("varchar(100)");

                entity.Property(e => e.SolicitudCelularProp).HasColumnType("varchar(100)");

                entity.Property(e => e.SolicitudClabe)
                    .HasColumnName("SolicitudCLABE")
                    .HasColumnType("varchar(18)");

                entity.Property(e => e.SolicitudCuenta).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudCuotaMant).HasColumnType("decimal(18,2)");

                entity.Property(e => e.SolicitudDepositoGarantia).HasColumnType("decimal(18,2)");

                entity.Property(e => e.SolicitudDestinoArrendamien).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudDomicilioProp).HasColumnType("varchar(500)");

                entity.Property(e => e.SolicitudEmail).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudEmailProp).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudEsAdminInmueble).HasColumnType("bit(1)");

                entity.Property(e => e.SolicitudEstatus).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudFiador).HasColumnType("bit(1)");

                entity.Property(e => e.SolicitudImporteMensual).HasColumnType("decimal(18,2)");

                entity.Property(e => e.SolicitudIncluidaRenta).HasColumnType("bit(1)");

                entity.Property(e => e.SolicitudInmuebleGaran).HasColumnType("bit(1)");

                entity.Property(e => e.SolicitudLugarFirma).HasColumnType("varchar(500)");

                entity.Property(e => e.SolicitudNacionalidad).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudNombreCuenta).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudNombreProp).HasColumnType("varchar(300)");

                entity.Property(e => e.SolicitudNumero).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudObservaciones).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudPagare).HasColumnType("bit(1)");

                entity.Property(e => e.SolicitudPersonaSolicita).HasColumnType("varchar(300)");

                entity.Property(e => e.SolicitudRazonSocial).HasColumnType("varchar(300)");

                entity.Property(e => e.SolicitudRecibodePago).HasColumnType("varchar(300)");

                entity.Property(e => e.SolicitudRepresentanteLegal).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudRfc)
                    .HasColumnName("SolicitudRFC")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.SolicitudTelefono).HasColumnType("varchar(100)");

                entity.Property(e => e.SolicitudTelefonoInmueble).HasColumnType("bit(1)");

                entity.Property(e => e.SolicitudTelefonoProp).HasColumnType("varchar(100)");

                entity.Property(e => e.SolicitudTipoDeposito).HasColumnType("varchar(200)");

                entity.Property(e => e.SolicitudTipoPoliza).HasColumnType("longtext");

                entity.Property(e => e.SolicitudUbicacionArrendado).HasColumnType("varchar(500)");

                entity.Property(e => e.TipoArrendatario).HasColumnType("varchar(200)");

                entity.Property(e => e.TipoIdent1).HasColumnType("varchar(40)");

                entity.Property(e => e.TipoIdent2).HasColumnType("varchar(40)");

                entity.Property(e => e.TipoIdent3).HasColumnType("varchar(40)");

                entity.Property(e => e.TipoIdentificacion).HasColumnType("varchar(40)");

                entity.Property(e => e.TitularNotaPoder).HasColumnType("varchar(200)");

                entity.HasOne(d => d.Asesor)
                    .WithMany(p => p.SolicitudAsesor)
                    .HasForeignKey(d => d.Asesorid)
                    .HasConstraintName("Solicitud_Usuarios_UsuariosId_fk_2");

                entity.HasOne(d => d.CentroCostos)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.CentroCostosId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Creador)
                    .WithMany(p => p.SolicitudCreador)
                    .HasForeignKey(d => d.Creadorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Solicitud_Usuarios_UsuariosId_fk_3");

                entity.HasOne(d => d.Estados)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.EstadosId);

                entity.HasOne(d => d.Representante)
                    .WithMany(p => p.SolicitudRepresentante)
                    .HasForeignKey(d => d.Representanteid)
                    .HasConstraintName("Solicitud_Usuarios_UsuariosId_fk");

                entity.HasOne(d => d.TipoInmobiliario)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.TipoInmobiliarioId);
            });

            modelBuilder.Entity<SolucionDetalle>(entity =>
            {
                entity.HasIndex(e => e.SolucionesId)
                    .HasName("SolucionDetalle_Soluciones_SolucionesId_fk");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("SolucionDetalle_Usuarios_UsuariosId_fk");

                entity.Property(e => e.SolucionDetalleId).HasColumnType("int(11)");

                entity.Property(e => e.DocumentoDesc).HasColumnType("longtext");

                entity.Property(e => e.DocumentosImagen).HasColumnType("longtext");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.SolucionesId).HasColumnType("int(11)");

                entity.Property(e => e.UsuarioId).HasColumnType("int(11)");

                entity.HasOne(d => d.Soluciones)
                    .WithMany(p => p.SolucionDetalle)
                    .HasForeignKey(d => d.SolucionesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SolucionDetalle_Soluciones_SolucionesId_fk");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.SolucionDetalle)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SolucionDetalle_Usuarios_UsuariosId_fk");
            });

            modelBuilder.Entity<Soluciones>(entity =>
            {
                entity.HasIndex(e => e.EstadoId)
                    .HasName("Soluciones_Estados_EstadosId_fk");

                entity.HasIndex(e => e.PolizaId)
                    .HasName("Soluciones_Poliza_PolizaId_fk");

                entity.HasIndex(e => e.ProcesoSolucionesId)
                    .HasName("Soluciones_ProcesoSoluciones_ProcesoSolucionesId_fk");

                entity.HasIndex(e => e.SolucionesId)
                    .HasName("Soluciones_SolucionesId_index");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("Soluciones_Usuarios_UsuariosId_fk");

                entity.Property(e => e.SolucionesId).HasColumnType("int(11)");

                entity.Property(e => e.AlcaldiaMunicipioInmueble).HasColumnType("varchar(100)");

                entity.Property(e => e.ApellidoMatArren).HasColumnType("varchar(40)");

                entity.Property(e => e.ApellidoMatFiador).HasColumnType("varchar(40)");

                entity.Property(e => e.ApellidoMatPro).HasColumnType("varchar(40)");

                entity.Property(e => e.ApellidoPatArren).HasColumnType("varchar(40)");

                entity.Property(e => e.ApellidoPatFiador).HasColumnType("varchar(40)");

                entity.Property(e => e.ApellidoPatPro).HasColumnType("varchar(40)");

                entity.Property(e => e.Argumenta).HasColumnType("varchar(1500)");

                entity.Property(e => e.CelularArrem).HasColumnType("varchar(40)");

                entity.Property(e => e.CelularFiador).HasColumnType("varchar(40)");

                entity.Property(e => e.CelularProp).HasColumnType("varchar(40)");

                entity.Property(e => e.ColoniaInmueble).HasColumnType("varchar(100)");

                entity.Property(e => e.Cpinmueble)
                    .HasColumnName("CPInmueble")
                    .HasColumnType("varchar(5)");

                entity.Property(e => e.DescripcionProblema).HasColumnType("varchar(1000)");

                entity.Property(e => e.DireccionInmueble).HasColumnType("varchar(200)");

                entity.Property(e => e.EmailArren).HasColumnType("varchar(40)");

                entity.Property(e => e.EmailFiador).HasColumnType("varchar(40)");

                entity.Property(e => e.EmailProp).HasColumnType("varchar(60)");

                entity.Property(e => e.EstadoId).HasColumnType("int(11)");

                entity.Property(e => e.FechaArrendatario).HasColumnType("date");

                entity.Property(e => e.FechaContratoF).HasColumnType("date");

                entity.Property(e => e.FechaContratoI).HasColumnType("date");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.NombreArren).HasColumnType("varchar(40)");

                entity.Property(e => e.NombreFiador).HasColumnType("varchar(40)");

                entity.Property(e => e.NombrePro).HasColumnType("varchar(40)");

                entity.Property(e => e.PolizaAnterior).HasColumnType("int(11)");

                entity.Property(e => e.PolizaId).HasColumnType("int(11)");

                entity.Property(e => e.ProcesoSolucionesId).HasColumnType("int(11)");

                entity.Property(e => e.TipoPoliza).HasColumnType("varchar(40)");

                entity.Property(e => e.UsuarioId).HasColumnType("int(11)");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Soluciones)
                    .HasForeignKey(d => d.EstadoId)
                    .HasConstraintName("Soluciones_Estados_EstadosId_fk");

                entity.HasOne(d => d.Poliza)
                    .WithMany(p => p.Soluciones)
                    .HasForeignKey(d => d.PolizaId)
                    .HasConstraintName("Soluciones_Poliza_PolizaId_fk");

                entity.HasOne(d => d.ProcesoSoluciones)
                    .WithMany(p => p.Soluciones)
                    .HasForeignKey(d => d.ProcesoSolucionesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Soluciones_ProcesoSoluciones_ProcesoSolucionesId_fk");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Soluciones)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Soluciones_Usuarios_UsuariosId_fk");
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.Property(e => e.TipoDocumentoId).HasColumnType("int(11)");

                entity.Property(e => e.TipoDocumentoDesc).HasColumnType("varchar(300)");
            });

            modelBuilder.Entity<TipoInmobiliario>(entity =>
            {
                entity.Property(e => e.TipoInmobiliarioId).HasColumnType("int(11)");

                entity.Property(e => e.Clausula).HasColumnType("varchar(4000)");

                entity.Property(e => e.TipoInmobiliarioDesc).HasColumnType("varchar(300)");
            });

            modelBuilder.Entity<TipoParentesco>(entity =>
            {
                entity.Property(e => e.TipoParentescoId).HasColumnType("int(11)");

                entity.Property(e => e.TipoParentescoDesc).HasColumnType("varchar(300)");
            });

            modelBuilder.Entity<TipoProceso>(entity =>
            {
                entity.Property(e => e.TipoProcesoId).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.Orden)
                    .HasColumnName("orden")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<TipoProcesoPo>(entity =>
            {
                entity.HasIndex(e => e.Orden)
                    .HasName("TipoProcesoPo_Orden_uindex")
                    .IsUnique();

                entity.Property(e => e.TipoProcesoPoId).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(40)");

                entity.Property(e => e.Orden).HasColumnType("int(11)");
            });

            modelBuilder.Entity<TipoRefComercial>(entity =>
            {
                entity.Property(e => e.TipoRefComercialId).HasColumnType("int(11)");

                entity.Property(e => e.TipoDetalleRc)
                    .HasColumnName("TipoDetalleRC")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.TipoRepresentaRc)
                    .HasColumnName("TipoRepresentaRC")
                    .HasColumnType("varchar(300)");
            });

            modelBuilder.Entity<TipoRefPersonal>(entity =>
            {
                entity.Property(e => e.TipoRefPersonalId).HasColumnType("int(11)");

                entity.Property(e => e.TipoRefPersonalDesc).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<UsuarioPoliza>(entity =>
            {
                entity.HasIndex(e => e.PolizaId)
                    .HasName("UsuarioPoliza_Poliza_PolizaId_fk");

                entity.HasIndex(e => e.TipoProcesoPoId)
                    .HasName("UsuarioPoliza_TipoProcesoPo_TipoProcesoPoId_fk");

                entity.HasIndex(e => e.UsuariosId)
                    .HasName("UsuarioPoliza_Usuarios_UsuariosId_fk");

                entity.Property(e => e.UsuarioPolizaId).HasColumnType("int(11)");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Observacion).HasColumnType("varchar(120)");

                entity.Property(e => e.PolizaId).HasColumnType("int(11)");

                entity.Property(e => e.TipoProcesoPoId).HasColumnType("int(11)");

                entity.Property(e => e.UsuariosId).HasColumnType("int(11)");

                entity.HasOne(d => d.Poliza)
                    .WithMany(p => p.UsuarioPoliza)
                    .HasForeignKey(d => d.PolizaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuarioPoliza_Poliza_PolizaId_fk");

                entity.HasOne(d => d.TipoProcesoPo)
                    .WithMany(p => p.UsuarioPoliza)
                    .HasForeignKey(d => d.TipoProcesoPoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuarioPoliza_TipoProcesoPo_TipoProcesoPoId_fk");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.UsuarioPoliza)
                    .HasForeignKey(d => d.UsuariosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuarioPoliza_Usuarios_UsuariosId_fk");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasIndex(e => e.AreaId);

                entity.HasIndex(e => e.RepresentacionId);

                entity.HasIndex(e => e.UsuarioPadreId)
                    .HasName("Usuarios_Usuarios_UsuariosId_fk");

                entity.Property(e => e.UsuariosId).HasColumnType("int(11)");

                entity.Property(e => e.Activo).HasColumnType("bit(1)");

                entity.Property(e => e.AreaId).HasColumnType("int(11)");

                entity.Property(e => e.ContraEncrypt).HasColumnType("longtext");

                entity.Property(e => e.Imagen).HasColumnType("longtext");

                entity.Property(e => e.IsResponsanle).HasColumnType("bit(1)");

                entity.Property(e => e.RepresentacionId).HasColumnType("int(11)");

                entity.Property(e => e.Titulo).HasColumnType("varchar(45)");

                entity.Property(e => e.UsuarioApellidoMaterno).HasColumnType("varchar(200)");

                entity.Property(e => e.UsuarioApellidoPaterno).HasColumnType("varchar(200)");

                entity.Property(e => e.UsuarioCelular).HasColumnType("varchar(100)");

                entity.Property(e => e.UsuarioContrasenia).HasColumnType("varchar(500)");

                entity.Property(e => e.UsuarioInmobiliaria).HasColumnType("varchar(300)");

                entity.Property(e => e.UsuarioNomCompleto).HasColumnType("varchar(300)");

                entity.Property(e => e.UsuarioNombre).HasColumnType("varchar(200)");

                entity.Property(e => e.UsuarioPadreId).HasColumnType("int(11)");

                entity.Property(e => e.UsuarioTelefono).HasColumnType("varchar(100)");

                entity.Property(e => e.UsurioEmail).HasColumnType("varchar(200)");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.AreaId);

                entity.HasOne(d => d.Representacion)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RepresentacionId);

                entity.HasOne(d => d.UsuarioPadre)
                    .WithMany(p => p.InverseUsuarioPadre)
                    .HasForeignKey(d => d.UsuarioPadreId)
                    .HasConstraintName("Usuarios_Usuarios_UsuariosId_fk");
            });

            modelBuilder.Entity<UsuariosSolicitud>(entity =>
            {
                entity.HasIndex(e => e.SolicitudId);

                entity.HasIndex(e => e.TipoProcesoId);

                entity.HasIndex(e => e.UsuariosId);

                entity.Property(e => e.UsuariosSolicitudId)
                    .HasColumnName("usuariosSolicitudId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Observacion).HasColumnType("varchar(500)");

                entity.Property(e => e.Proceso).HasColumnType("varchar(500)");

                entity.Property(e => e.SolicitudId).HasColumnType("int(11)");

                entity.Property(e => e.TipoProcesoId).HasColumnType("int(11)");

                entity.Property(e => e.UsuariosId).HasColumnType("int(11)");

                entity.HasOne(d => d.Solicitud)
                    .WithMany(p => p.UsuariosSolicitud)
                    .HasForeignKey(d => d.SolicitudId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuariosSolicitud_Solicitud_SolicitudId_fk");

                entity.HasOne(d => d.TipoProceso)
                    .WithMany(p => p.UsuariosSolicitud)
                    .HasForeignKey(d => d.TipoProcesoId);

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.UsuariosSolicitud)
                    .HasForeignKey(d => d.UsuariosId);
            });

            modelBuilder.Entity<UsuariosSoluciones>(entity =>
            {
                entity.HasKey(e => e.UsuariosSolulucionesId);

                entity.HasIndex(e => e.ProcesoSolucionesId)
                    .HasName("IX_UsuariosSoluciones_TipoProcesoId");

                entity.HasIndex(e => e.SolucionesId)
                    .HasName("IX_UsuariosSoluciones_SolicitudId");

                entity.HasIndex(e => e.UsuariosId);

                entity.Property(e => e.UsuariosSolulucionesId).HasColumnType("int(11)");

                entity.Property(e => e.Observacion).HasColumnType("varchar(500)");

                entity.Property(e => e.ProcesoSolucionesId).HasColumnType("int(11)");

                entity.Property(e => e.SolucionesId).HasColumnType("int(11)");

                entity.Property(e => e.UsuariosId).HasColumnType("int(11)");

                entity.HasOne(d => d.ProcesoSoluciones)
                    .WithMany(p => p.UsuariosSoluciones)
                    .HasForeignKey(d => d.ProcesoSolucionesId);

                entity.HasOne(d => d.Soluciones)
                    .WithMany(p => p.UsuariosSoluciones)
                    .HasForeignKey(d => d.SolucionesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuariosSoluciones_Soluciones_SolucionesId_fk");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.UsuariosSoluciones)
                    .HasForeignKey(d => d.UsuariosId);
            });
        }
    }
}
