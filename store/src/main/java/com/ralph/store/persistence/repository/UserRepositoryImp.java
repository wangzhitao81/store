package com.ralph.store.persistence.repository;

import org.hibernate.SessionFactory;
import org.hibernate.criterion.Restrictions;
import org.springframework.stereotype.Repository;

import com.ralph.store.persistence.entity.User;

@Repository
public class UserRepositoryImp implements UserRepository {
	private SessionFactory sessionFactory;

	/*public UserRepositoryImp(SessionFactory sessionFactory) {
		this.sessionFactory = sessionFactory;
	}*/
	
	public User Login(String username,String password){
		Object user = sessionFactory.getCurrentSession()
				.createCriteria(User.class)
				.add(Restrictions.eq("LoginName", "loginname"))
				.add(Restrictions.eq("Password", "password"))
				.uniqueResult();
		if(user!=null){
			return (User)user;
		}else{
			return null;
		}
	}
}
