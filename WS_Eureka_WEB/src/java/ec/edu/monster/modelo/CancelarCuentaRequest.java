/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.modelo;

import java.io.Serializable;

/**
 *
 * @author ckan1
 */
public class CancelarCuentaRequest implements Serializable {
    private String clave;
    private boolean cancelar;

    public CancelarCuentaRequest() {}

    public CancelarCuentaRequest(String clave, boolean cancelar) {
        this.clave = clave;
        this.cancelar = cancelar;
    }

    public String getClave() {
        return clave;
    }

    public void setClave(String clave) {
        this.clave = clave;
    }

    public boolean isCancelar() {
        return cancelar;
    }

    public void setCancelar(boolean cancelar) {
        this.cancelar = cancelar;
    }
}
