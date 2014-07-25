package com.ralph.store.persistence.repository;

import com.ralph.store.persistence.entity.User;

public interface UserRepository {
	User Login(String username,String password);
}
