/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.modelo;

/**
 *
 * @author Jimena
 */
public class CuentaRequest {
    private String monedaCodigo;
    private String sucursalCodigo;
    private String empleadoCodigo;
    private String clienteCodigo;
    private String clave;

    public CuentaRequest() {}

    public CuentaRequest(String monedaCodigo, String sucursalCodigo, String empleadoCodigo, String clienteCodigo, String clave) {
        this.monedaCodigo = monedaCodigo;
        this.sucursalCodigo = sucursalCodigo;
        this.empleadoCodigo = empleadoCodigo;
        this.clienteCodigo = clienteCodigo;
        this.clave = clave;
    }

    public String getMonedaCodigo() { return monedaCodigo; }
    public String getSucursalCodigo() { return sucursalCodigo; }
    public String getEmpleadoCodigo() { return empleadoCodigo; }
    public String getClienteCodigo() { return clienteCodigo; }
    public String getClave() { return clave; }
}
