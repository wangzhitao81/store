package com.ralph.store.web.controller;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import com.ralph.store.core.service.LoginService;

@Controller
public class LoginController  {
	protected final Log logger= LogFactory.getLog(getClass());
	
	@Autowired
	private LoginService loginService;
	
	@RequestMapping("/showLogin")
	public String showLogin(Model model) {
		logger.info("Returning login view");
		
		return "login";
	}
	
	
}
