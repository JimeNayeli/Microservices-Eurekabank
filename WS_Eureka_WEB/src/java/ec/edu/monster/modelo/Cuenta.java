/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.modelo;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 *
 * @author ckan1
 */
public class Cuenta implements Serializable {
        private String monedaCodigo;
    private String sucursalCodigo;
    private String empleadoCodigo;
    private String clienteCodigo;
    private String clave;

    public Cuenta() {}

    public Cuenta(String monedaCodigo, String sucursalCodigo, String empleadoCodigo, String clienteCodigo, String clave) {
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

    public void setMonedaCodigo(String monedaCodigo) {
        this.monedaCodigo = monedaCodigo;
    }

    public void setSucursalCodigo(String sucursalCodigo) {
        this.sucursalCodigo = sucursalCodigo;
    }

    public void setEmpleadoCodigo(String empleadoCodigo) {
        this.empleadoCodigo = empleadoCodigo;
    }

    public void setClienteCodigo(String clienteCodigo) {
        this.clienteCodigo = clienteCodigo;
    }

    public void setClave(String clave) {
        this.clave = clave;
    }
    
    
}
