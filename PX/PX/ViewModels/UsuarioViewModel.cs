﻿using PX.Base;
using PX.Models;
using PX.Repositories;
using PX.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PX.ViewModels
{
    public class UsuarioViewModel : ViewModelBase
    {
        RepositoryUsuarios repo;
        public UsuarioViewModel()
        {
            this.repo = new RepositoryUsuarios();
            if (Usuario == null)
            {
                this.Usuario = new Usuario();
            }
        }

        private Usuario _Usuario;
        public Usuario Usuario
        {
            get
            {
                return this._Usuario;
            }
            set
            {
                this._Usuario = value;
                OnPropertyChange("Usuario");
            }
        }
        public Command IniciarSesion
        {
            get
            {
                return new Command(async () =>
                {
                    String token = await Tools.Api.GetToken(this.Usuario.Username, this.Usuario.Password);
                    if (token == null || token=="")
                    {
                        this.Mensaje = "El nombre de usuario o contraseña son incorrectos";
                    }
                    else
                    {
                        //this.Mensaje = "";
                        SessionService session = App.Locator.SessionService;
                        session.Cadena = token;
                        session.Usuario = this.Usuario;
                    }
                   /* await this.repo.InsertarDoctor(this.Doctor);
                    MessagingCenter.Send<DoctoresViewModel>(App.Locator.DoctoresViewModel, "INSERTAR");*/
                });
            }
        }
        private string _Mensaje;
        public string Mensaje
        {
            get
            {
                return this._Mensaje;
            }
            set
            {
                this._Mensaje = value;
                OnPropertyChange("Mensaje");
            }
        }
    }
}
