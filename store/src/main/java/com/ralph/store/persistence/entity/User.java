package com.ralph.store.persistence.entity;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;



@Entity
public class User {
	@Id
	//@SequenceGenerator(name="teacherSEQ", sequenceName="teacherSEQ_DB")
	//@GeneratedValue(strategy=GenerationType.TABLE, generator="Teacher_GEN")
	private int Id;
	
	@Column(name="UserName", nullable=false,length=50)
	private String UserName;
	
	@Column(name="Password", nullable=false,length=50)
	private String Password;
}
