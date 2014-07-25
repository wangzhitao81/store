package com.ralph.store.web.controller.profile;

import java.util.Date;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
public class ProfileController {
	protected final Log logger = LogFactory.getLog(getClass());
	
	@RequestMapping("/profile/myprofile")
	public String MyProfile(String name, Model model) {
		logger.info("Returning myprofile view");
		
		model.addAttribute("nowtime",new Date().toString());
		 
		return "/profile/myprofile";
	}
}
