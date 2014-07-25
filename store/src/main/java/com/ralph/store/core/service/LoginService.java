package com.ralph.store.core.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ralph.store.persistence.repository.UserRepository;

@Service
public class LoginService {
	@Autowired
	private UserRepository userRepository;
	
	public boolean Login(String username,String password){
		if(userRepository.Login(username, password)!=null){
			return true;
		}else{
			return false;
		}
	}
}
