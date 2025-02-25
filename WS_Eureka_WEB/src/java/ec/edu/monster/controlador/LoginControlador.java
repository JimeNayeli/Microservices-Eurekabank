/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.controlador;

import ec.edu.monster.modelo.EurekabankService;
import ec.edu.monster.modelo.LoginResponse;
import ec.edu.monster.modelo.LoginService;
import javax.ws.rs.ClientErrorException;
import javax.ws.rs.core.Response;

/**
 *
 * @author ckan1
 */
public class LoginControlador {
    private LoginService eurekabankService;

    public LoginControlador() {
        this.eurekabankService = new LoginService();
    }

    public LoginResponse login(String usuario, String password, String tipoUsuario) {
        try {
            return this.eurekabankService.login(usuario, password, tipoUsuario);
        } catch (ClientErrorException e) {
            System.out.println("Error de cliente: " + e.getMessage());
            LoginResponse errorResponse = new LoginResponse();
            errorResponse.setMensaje("Error de conexión: " + e.getMessage());
            return errorResponse;
        } catch (Exception e) {
            System.out.println("Error inesperado: " + e.getMessage());
            LoginResponse errorResponse = new LoginResponse();
            errorResponse.setMensaje("Error inesperado: " + e.getMessage());
            return errorResponse;
        }
    }
    
}
