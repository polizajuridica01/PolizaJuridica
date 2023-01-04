using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;
using PolizaJuridica.Utilerias;
using PolizaJuridica.ViewModels;
using System.Net.Mail;
using System.Net;


namespace PolizaJuridica.Webservices
{

    [Route("api/[controller]")]
    [ApiController]
    public class EventoUsuariosController : ControllerBase
    {
        private readonly PolizaJuridicaDbContext _context;

        public EventoUsuariosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // POST: api/EventoUsuarios
        [HttpPost]
        public async Task<IActionResult> PostEventoUsuarios([FromBody] EventoUsuarios eu)
        {
            string result = string.Empty;
            List<ErroresViewModel> mensaje = new List<ErroresViewModel>();
            Boolean bandera = false;
            EnviarCorreo correo = new EnviarCorreo(_context);

            if (eu.Correo == null)
            {
                mensaje.Add(Mensajes.ErroresAtributos("Correo"));
                bandera = true;
            }

            if(eu.Nombre == null)
            {
                mensaje.Add(Mensajes.ErroresAtributos("Nombre"));
                bandera = true;
            }

            if (eu.ApellidoPaterno == null)
            {
                mensaje.Add(Mensajes.ErroresAtributos("ApellidoPaterno"));
                bandera = true;
            }

            if (eu.EventosId == null)
            {
                mensaje.Add(Mensajes.MensajesError("Evento Id"));
                bandera = true;
            }

            if( eu.RepresentanteId == 0 &&  eu.EstadoId == 0)
            {
                mensaje.Add(Mensajes.MensajesError("Favor de seleccionar un Estado o un representante de Póliza Jurídica"));
                bandera = true;
            }

            if (bandera == false)
            {
                var evento = _context.Eventos.Where(e => e.EventosId == eu.EventosId).FirstOrDefault();

                var existe = _context.EventoUsuarios.Where(e => e.EventosId == eu.EventosId).Where(e => e.Correo == eu.Correo.Trim()).FirstOrDefault();
                if (existe == null)
                {
                    _context.EventoUsuarios.Add(eu);
                    await _context.SaveChangesAsync();

                    var usuario = _context.Usuarios.FirstOrDefault(u => u.UsurioEmail == eu.Correo);
                    System.Random _random = new Random();
                    var cantidad = _context.EventoUsuarios.Where(c => c.EventosId == eu.EventosId).Count();
                    if (usuario != null)
                    {


                        if (cantidad <=950)
                        {
                            mensaje.Add(Mensajes.Exitoso("Id de reunión: " + evento.ReunionIde.Trim() + " Contraseña: " + evento.ReunionPass.Trim() + " Url: " + evento.ReunionUr+ " Adicional recibira un correo con las instrucciones de la plática."));
                            correo.EnviarCorreoSendGrid(eu, evento);
                        }
                        else
                        {
                            mensaje.Add(Mensajes.Exitoso("Gracias por su registro, pero el cupo de la sesión ha llegado a su limite, para nuestra proxima plática recibiras un correo con las instrucciones."));
                        }

                    }
                    else
                    {
                        if (eu.RepresentanteId > 0)
                        {
                            var representante = _context.Usuarios.FirstOrDefault(us => us.UsuariosId == eu.RepresentanteId);
                            Usuarios u = new Usuarios
                            {
                                UsurioEmail = eu.Correo,
                                UsuarioNombre = eu.Nombre,
                                UsuarioNomCompleto = eu.Nombre + " " + eu.ApellidoPaterno,
                                UsuarioCelular = eu.Celular,
                                UsuarioContrasenia = "PJ58080",
                                AreaId = 2,//Asesores
                                UsuarioPadreId = eu.RepresentanteId,
                                RepresentacionId =representante.RepresentacionId

                            };
                            _context.Usuarios.Add(u);
                            var resulta = await _context.SaveChangesAsync();
                            if (resulta > 0)
                            {

                            }
                            else
                            {
                                Log l = new Log
                                {
                                    LogFecha = DateTime.Now,
                                    LogObjetoIn = resulta.ToString(),
                                    LogProceso = "WS Evento Usuarios"
                                };

                                _context.Add(l);
                                _context.SaveChanges();

                            }

                        }
                     
                        if (cantidad <= 950)
                        {
                            mensaje.Add(Mensajes.Exitoso("Id de reunión: " + evento.ReunionIde.Trim() + " Contraseña: " + evento.ReunionPass.Trim() + " Url: " + evento.ReunionUr + " Adicional recibira un correo con las instrucciones de la plática."));
                            correo.EnviarCorreoSendGrid(eu, evento);
                        }
                        else
                        {
                            mensaje.Add(Mensajes.Exitoso("Gracias por su registro, pero el cupo de la sesión ha llegado a su limite, para nuestra proxima plática recibiras un correo con las instrucciones."));
                        }
                    }
                    
                }
                else
                {
                    mensaje.Add(Mensajes.Exitoso("Id de reunión: " + evento.ReunionIde.Trim() + " Contraseña: " + evento.ReunionPass.Trim() + " Url: " + evento.ReunionUr + " Adicional recibira un correo con las instrucciones de la plática."));
                    correo.EnviarCorreoSendGrid(eu, evento);
                }
                
            }

            return Ok(mensaje);
        }
    }
}