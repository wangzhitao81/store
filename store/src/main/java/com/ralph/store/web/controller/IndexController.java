/**
 * 
 */
package com.ralph.store.web.controller;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

/**
 * @author Ralph
 *
 */
@Controller
public class IndexController {
	protected final Log logger = LogFactory.getLog(getClass());
	
	@RequestMapping({"/index","/"})
	public String showHomePage(@RequestParam(value="name", required=false, defaultValue="World") String name, Model model) {
		logger.info("Returning index view"); 
		model.addAttribute("name", name);
		 
		return "index";
	}
	
}
