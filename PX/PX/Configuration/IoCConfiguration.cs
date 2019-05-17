using Autofac;
using PX.Services;
using PX.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PX.Configuration
{
    public class IoCConfiguration
    {
        private IContainer container;
        public IoCConfiguration()
        {
            ContainerBuilder builder = new ContainerBuilder();
            //REGISTRAMOS TODOS LOS TIPOS DE INYECCION DE DEPENDENCIAS
            //O CLASES QUE NECESITAMOS QUE NOS DEVUELVA EL CONTENEDOR
            //CLASES COMUNICADAS ENTRE OTRAS
            builder.RegisterType<SessionService>().SingleInstance();
            builder.RegisterType<ProductosViewModel>();
            builder.RegisterType<CarritoViewModel>();
            builder.RegisterType<ItemsMenuViewModel>().SingleInstance();
            this.container = builder.Build();
        }

        //CREAMOS METODOS PARA QUE EL CONTENEDOR SEA CAPAZ
        //DE DEVOLVER LAS CLASES QUE LLAMEMOS DE FORMA EXPLICITA
        public ProductosViewModel ProductosViewModel
        {
            get
            {
                return this.container.Resolve<ProductosViewModel>();
            }
        }

        public SessionService SessionService
        {
            get
            {
                return this.container.Resolve<SessionService>();
            }
        }

        public CarritoViewModel CarritoViewModel
        {
            get
            {
                return this.container.Resolve<CarritoViewModel>();
            }
        }

        public ItemsMenuViewModel ItemsMenuViewModel
        {
            get
            {
                return this.container.Resolve<ItemsMenuViewModel>();
            }
        }
    }
}
