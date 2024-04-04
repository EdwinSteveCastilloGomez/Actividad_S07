using ListaBlazorApp.Models;
using Microsoft.VisualBasic;
using System.Collections;

namespace ListaBlazorApp.Services
{
    public class ListaEnlazadaSimple : IEnumerable
    {
        public Nodo PrimerNodo { get; set; }
        public Nodo UltimoNodo { get; set; }
      
        public ListaEnlazadaSimple()
        {
            PrimerNodo = null;
            UltimoNodo = null;
        }

        public bool ListaVacia()
        {
            return PrimerNodo == null;
        }

        public string AgregarNodoAlFinal(Nodo nuevoNodo)
        {
            if (ListaVacia())
            {
                PrimerNodo = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            else
            {
                UltimoNodo.Referencia = nuevoNodo;      //el ultimo nodo en la lista le estamos pasando la referencia del nuevo nodo 
                UltimoNodo = nuevoNodo;
            }

            return "Se ha agregado el nodo al final";
        }        
        
        public string AgregarNodoAlInicio(Nodo nuevoNodo)
        {
            if (ListaVacia())
            {
                PrimerNodo = nuevoNodo;
                UltimoNodo = nuevoNodo;
            }
            else
            {
                nuevoNodo.Referencia = PrimerNodo;
                PrimerNodo = nuevoNodo;

            }

            return "Se ha agregado el nodo al inicio";
        }


        public string AgregarAntesDe(object valorExistente, Nodo nuevoNodo)
        {

            if (ListaVacia())
            {

                return "La lista esta vacia, no se puede agregar antes de un elemento.";
            }

            Nodo nodoActual = PrimerNodo;
            Nodo nodoAnterior = null;

            while (nodoActual != null)
            {
                if(nodoActual.Informacion.Equals(valorExistente))
                {
                    if(nodoAnterior == null)
                    {
                        nuevoNodo.Referencia= PrimerNodo;
                        PrimerNodo=nuevoNodo;
                    }
                    else
                    {
                        nuevoNodo.Referencia = nodoActual;
                        nodoAnterior.Referencia = nuevoNodo;
                    }
                    return $"Se ha agregado el nodo antes del elemento con valor {valorExistente}";
                }
                    nodoAnterior= nodoActual;
                    nodoActual=nodoActual.Referencia;
            }
            return $"No se encontro el elemenoto con valor {valorExistente} en la lista";
        }


        public string AgregarNodoDespuesDe(Nodo nodoExistente, Nodo nuevoNodo)
        {
            if(nodoExistente == null)
            {
                return "El nodo existente no puede ser nulo.";
            }

            if(nodoExistente== UltimoNodo)
            {
                //si el nodo existente es el ultimo nodo, simplemente agregamos el nuebo nodo al final
                return AgregarNodoAlFinal(nuevoNodo);
            }

            nuevoNodo.Referencia = nodoExistente.Referencia;
            nodoExistente.Referencia = nuevoNodo;
            return $"Se ha agregado el nodo despues del nodo con valor {nodoExistente.Informacion}";
        }


        public string AgregarEnPosicion(int posicion, Nodo nuevoNodo)
        {
            if(posicion<0)
            {
                return "La posición no puede ser negativa.";
            }
            if (posicion == 0)
            {
                return AgregarNodoAlInicio(nuevoNodo);
            }

            int indice = 0;
            Nodo nodoActual = PrimerNodo;

            while (nodoActual != null && indice < posicion - 1)
            {
                nodoActual = nodoActual.Referencia;
                indice++;
            }

            if (nodoActual == null)
            {
                return $"La lista tiene menos de {posicion} elementos.";
            }

            nuevoNodo.Referencia = nodoActual.Referencia;
            nodoActual.Referencia = nuevoNodo;

            if (nodoActual == UltimoNodo)
            {
                UltimoNodo = nuevoNodo;
            }

            return $"Se ha agregado el nodo en la posición {posicion}.";
        }

        public string InsertarAntesDePosicion(int posicion, Nodo nuevoNodo)
        {
            if (posicion <= 0)
            {
                return "La posición debe ser mayor que 0.";
            }

            if (posicion == 1)
            {
                return AgregarNodoAlInicio(nuevoNodo);
            }

            int indice = 1;  // Inicializamos el índice en 1, ya que estamos buscando el nodo antes de la posición especificada
            Nodo nodoActual = PrimerNodo;

            while (nodoActual != null && indice < posicion - 1)  // Avanzamos hasta el nodo antes de la posición deseada
            {
                nodoActual = nodoActual.Referencia;
                indice++;
            }

            if (nodoActual == null || nodoActual.Referencia == null)
            {
                return $"La lista tiene menos de {posicion - 1} elementos.";
            }

            nuevoNodo.Referencia = nodoActual.Referencia;  // Establecemos la referencia del nuevo nodo al nodo siguiente
            nodoActual.Referencia = nuevoNodo;  // Establecemos la referencia del nodo actual al nuevo nodo

            return $"Se ha insertado el nodo antes de la posición {posicion}.";
        }

        public string InsertarDespuesDePosicion(int posicion, Nodo nuevoNodo)
        {
            if (posicion < 0)
            {
                return "La posición no puede ser negativa.";
            }

            int indice = 0;
            Nodo nodoActual = PrimerNodo;

            while (nodoActual != null && indice < posicion)  // Avanzamos hasta el nodo en la posición deseada
            {
                nodoActual = nodoActual.Referencia;
                indice++;
            }

            if (nodoActual == null)
            {
                return $"La lista tiene menos de {posicion} elementos.";
            }

            nuevoNodo.Referencia = nodoActual.Referencia;  // Establecemos la referencia del nuevo nodo al nodo siguiente
            nodoActual.Referencia = nuevoNodo;  // Establecemos la referencia del nodo actual al nuevo nodo insertado

            if (nodoActual == UltimoNodo)  // Si el nodo actual es el último nodo, actualizamos UltimoNodo al nuevo nodo
            {
                UltimoNodo = nuevoNodo;
            }

            return $"Se ha insertado el nodo después de la posición {posicion}.";
        }

        public string EliminarNodoAlInicio()
        {
            if (ListaVacia())
            {
                return "La lista está vacía, no se puede eliminar ningún nodo.";
            }

            Nodo nodoEliminado = PrimerNodo;

            if (PrimerNodo == UltimoNodo)
            {
                PrimerNodo = null;
                UltimoNodo = null;
            }
            else
            {
                PrimerNodo = PrimerNodo.Referencia;
            }

            return $"Se ha eliminado el nodo al inicio con el valor '{nodoEliminado.Informacion}'.";
        }

        public string EliminarNodoAlFinal()
        {
            if (ListaVacia())
            {
                return "La lista está vacía, no se puede eliminar ningún nodo.";
            }

            Nodo nodoEliminado = UltimoNodo;

            if (PrimerNodo == UltimoNodo)
            {
                PrimerNodo = null;
                UltimoNodo = null;
            }
            else
            {
                Nodo nodoActual = PrimerNodo;
                while (nodoActual.Referencia != UltimoNodo)
                {
                    nodoActual = nodoActual.Referencia;
                }
                nodoActual.Referencia = null;
                UltimoNodo = nodoActual;
            }

            return $"Se ha eliminado el nodo al final con el valor '{nodoEliminado.Informacion}'.";
        }

        public string EliminarNodoAntesDe(object valorExistente)
        {
            if (ListaVacia())
            {
                return "La lista está vacía, no se puede eliminar ningún nodo.";
            }

            Nodo nodoActual = PrimerNodo;
            Nodo nodoAnterior = null;

            while (nodoActual != null)
            {
                if (nodoActual.Informacion.Equals(valorExistente))
                {
                    if (nodoAnterior == null)
                    {
                        return $"No se puede eliminar un nodo antes del primer nodo.";
                    }
                    else
                    {
                        nodoAnterior.Referencia = nodoActual.Referencia;
                        return $"Se ha eliminado el nodo antes del elemento con valor '{valorExistente}'.";
                    }
                }
                nodoAnterior = nodoActual;
                nodoActual = nodoActual.Referencia;
            }

            return $"No se encontró el elemento con valor '{valorExistente}' en la lista.";
        }

        public string EliminarNodoDespuesDe(object valorExistente)
        {
            if (ListaVacia())
            {
                return "La lista está vacía, no se puede eliminar ningún nodo.";
            }

            Nodo nodoActual = PrimerNodo;

            while (nodoActual != null)
            {
                if (nodoActual.Informacion.Equals(valorExistente))
                {
                    if (nodoActual == UltimoNodo)
                    {
                        return $"No se puede eliminar un nodo después del último nodo.";
                    }
                    else
                    {
                        Nodo nodoEliminado = nodoActual.Referencia;
                        nodoActual.Referencia = nodoActual.Referencia.Referencia;
                        return $"Se ha eliminado el nodo después del elemento con valor '{valorExistente}'.";
                    }
                }
                nodoActual = nodoActual.Referencia;
            }

            return $"No se encontró el elemento con valor '{valorExistente}' en la lista.";
        }

        public string EliminarNodoEnPosicion(int posicion)
        {
            if (posicion < 0)
            {
                return "La posición no puede ser negativa.";
            }

            if (ListaVacia())
            {
                return "La lista está vacía, no se puede eliminar ningún nodo.";
            }

            if (posicion == 0)
            {
                Nodo nodoEliminado = PrimerNodo;
                PrimerNodo = PrimerNodo.Referencia;
                return $"Se ha eliminado el nodo en la posición {posicion} con el valor '{nodoEliminado.Informacion}'.";
            }

            int indice = 0;
            Nodo nodoActual = PrimerNodo;
            Nodo nodoAnterior = null;

            while (nodoActual != null && indice < posicion)
            {
                nodoAnterior = nodoActual;
                nodoActual = nodoActual.Referencia;
                indice++;
            }

            if (nodoActual == null)
            {
                return $"La lista tiene menos de {posicion + 1} elementos.";
            }

            nodoAnterior.Referencia = nodoActual.Referencia;

            if (nodoActual == UltimoNodo)
            {
                UltimoNodo = nodoAnterior;
            }

            return $"Se ha eliminado el nodo en la posición {posicion} con el valor '{nodoActual.Informacion}'.";
        }

        public string RecorrerRecursivo(Nodo nodoActual)
        {
            if (nodoActual == null)
            {
                return string.Empty;
            }

            return $"{nodoActual.Informacion} -> {RecorrerRecursivo(nodoActual.Referencia)}";
        }

        public Nodo BusquedaEspecifica(Object infobuscada)
        {
            Nodo nodoActual = PrimerNodo;
            string salida;

            while (nodoActual != null)
            {
                if (nodoActual.Informacion.Equals(infobuscada))
                {
                    return nodoActual; // Retorna el nodo si se encuentra el valor buscado
                }
                nodoActual = nodoActual.Referencia;
            }

            return null; // Retorna null si el valor no se encuentra en la lista
        }
        public void OrdenarLista()
        {
            if (PrimerNodo == null || PrimerNodo.Referencia == null)
            {
                // La lista está vacía o tiene solo un elemento, por lo que no se necesita ordenar.
                return;
            }

            // Se utiliza el algoritmo de ordenamiento de selección para ordenar la lista.
            Nodo actual = PrimerNodo;
            while (actual != null)
            {
                Nodo min = actual;
                Nodo siguiente = actual.Referencia;
                while (siguiente != null)
                {
                    // Si el valor del siguiente nodo es menor que el valor del nodo actual mínimo, se actualiza el nodo mínimo.
                    if (Convert.ToInt32(siguiente.Informacion) < Convert.ToInt32(min.Informacion))
                    {
                        min = siguiente;
                    }
                    siguiente = siguiente.Referencia;
                }

                // Se intercambian los valores del nodo actual y del nodo mínimo si no son el mismo nodo.
                if (min != actual)
                {
                    object temp = actual.Informacion;
                    actual.Informacion = min.Informacion;
                    min.Informacion = temp;
                }
                actual = actual.Referencia;
            }
        }


        public IEnumerator GetEnumerator()
        {
            Nodo nodoAuxiliar = PrimerNodo;

            while (nodoAuxiliar != null)
            {
                yield return nodoAuxiliar;
                nodoAuxiliar = nodoAuxiliar.Referencia;
            }
        }
    }
}
