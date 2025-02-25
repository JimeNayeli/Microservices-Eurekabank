/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.modelo;

/**
 *
 * @author Jimena
 */
public class CuentaDetalleResponse {
    private String codigoCuenta;
    private String moneda;
    private String sucursal;
    private String ciudadSucursal;
    private String nombreEmpleado;
    private String dniCliente;
    private String nombreCliente;
    private String ciudadCliente;
    private String direccionCliente;
    private String telefonoCliente;
    private String emailCliente;
    private String fechaCreacion;
    private int numeroMovimientos;
    private double saldo;

    // Getters y Setters
    public String getCodigoCuenta() { return codigoCuenta; }
    public void setCodigoCuenta(String codigoCuenta) { this.codigoCuenta = codigoCuenta; }

    public String getMoneda() { return moneda; }
    public void setMoneda(String moneda) { this.moneda = moneda; }

    public String getSucursal() { return sucursal; }
    public void setSucursal(String sucursal) { this.sucursal = sucursal; }

    public String getCiudadSucursal() { return ciudadSucursal; }
    public void setCiudadSucursal(String ciudadSucursal) { this.ciudadSucursal = ciudadSucursal; }

    public String getNombreEmpleado() { return nombreEmpleado; }
    public void setNombreEmpleado(String nombreEmpleado) { this.nombreEmpleado = nombreEmpleado; }

    public String getDniCliente() { return dniCliente; }
    public void setDniCliente(String dniCliente) { this.dniCliente = dniCliente; }

    public String getNombreCliente() { return nombreCliente; }
    public void setNombreCliente(String nombreCliente) { this.nombreCliente = nombreCliente; }

    public String getCiudadCliente() { return ciudadCliente; }
    public void setCiudadCliente(String ciudadCliente) { this.ciudadCliente = ciudadCliente; }

    public String getDireccionCliente() { return direccionCliente; }
    public void setDireccionCliente(String direccionCliente) { this.direccionCliente = direccionCliente; }

    public String getTelefonoCliente() { return telefonoCliente; }
    public void setTelefonoCliente(String telefonoCliente) { this.telefonoCliente = telefonoCliente; }

    public String getEmailCliente() { return emailCliente; }
    public void setEmailCliente(String emailCliente) { this.emailCliente = emailCliente; }

    public String getFechaCreacion() { return fechaCreacion; }
    public void setFechaCreacion(String fechaCreacion) { this.fechaCreacion = fechaCreacion; }

    public int getNumeroMovimientos() { return numeroMovimientos; }
    public void setNumeroMovimientos(int numeroMovimientos) { this.numeroMovimientos = numeroMovimientos; }

    public double getSaldo() { return saldo; }
    public void setSaldo(double saldo) { this.saldo = saldo; }
}
