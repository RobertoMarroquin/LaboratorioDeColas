# Simulación de Colas Únicas Prioritarias para Gestión de Pacientes

Este sistema simula la gestión de pacientes en un entorno hospitalario utilizando una **cola única prioritaria** con reordenamiento dinámico basado en probabilidades y prioridades. El objetivo es modelar un flujo de atención más realista, donde la prioridad de cada paciente se determina principalmente por la edad, pero puede ser extendida a otros factores.

## Características principales

- **Cola única con prioridad dinámica:**  
  Todos los pacientes ingresan a una sola cola de registro. El orden de atención no es estrictamente FIFO, sino que se ajusta dinámicamente según la prioridad de cada paciente.

- **Priorización por peso:**  
  Cada paciente recibe un peso de prioridad (`PesoPrioridad`) calculado principalmente en función de la edad (mayor prioridad para niños y adultos mayores). Este sistema puede ser extendido para considerar otros factores.

- **Reordenamiento probabilístico:**  
  El sistema utiliza un algoritmo probabilístico (`OrdenadorProbabilisticoColaUnicaPrioritaria`) que reordena la cola de pacientes en cada ciclo, según las probabilidades asignadas a cada nivel de prioridad:
    - Prioridad alta: 60% de probabilidad de avanzar
    - Prioridad media: 30%
    - Prioridad baja: 10%

- **Asignación y gestión de servicios:**  
  Al ser atendido, cada paciente puede ser asignado a uno o varios servicios (Laboratorio, Radiología, Rehabilitación), y es colocado en la cola correspondiente según el servicio de mayor prioridad.

- **Atención y avance entre servicios:**  
  Al atender a un paciente en un servicio, si tiene servicios pendientes, es reasignado automáticamente a la siguiente cola de servicio según la prioridad de los servicios restantes.

- **Visualización en tiempo real:**  
  El sistema muestra el estado de las colas de registro y de cada servicio en tiempo real.

## Flujo general

1. **Ingreso de pacientes:**  
   Los pacientes se agregan a la cola única y son reordenados automáticamente según su prioridad y las probabilidades definidas.

2. **Registro y asignación de servicios:**  
   Al registrar un paciente, se seleccionan los servicios requeridos. El paciente es asignado a la cola del servicio con mayor peso.

3. **Atención en servicios:**  
   Al atender a un paciente en un servicio, si tiene servicios pendientes, es reasignado a la siguiente cola de servicio.

4. **Visualización:**  
   El sistema actualiza y muestra en pantalla el estado de todas las colas.

## Personalización

- Las probabilidades de avance por prioridad pueden ser ajustadas.
- El cálculo de prioridad puede ser extendido para considerar más factores además de la edad.

---

Este sistema es ideal para simular y analizar la eficiencia de modelos de atención con colas únicas y prioridades dinámicas en entornos hospitalarios.
