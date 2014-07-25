package com.ralph.store.web.domain;

import java.io.Serializable;

import javax.validation.constraints.NotNull;

import org.hibernate.validator.constraints.NotEmpty;





public class LoginInfo implements Serializable {
	  @NotNull
	  @NotEmpty
	  private String username;

	  @NotNull
	  @NotEmpty
	  private String password;
}
