
$("#country").change(function () {
		debugger;
		$("#state").empty();
		var id = $("#country").val();
		$.ajax({
			type: 'GET',
			url: '/Account/LoadState',
			data: { id: id },
			success: function (states) {
				debugger;
				$.each(states, function (i, state) {
					$("#state").append('<option value="' + state.id + ' ">' + state.name + '</option>');
				});
			},
			Error: function (ex) {
				alert('Failed to retreive state .' + ex);
			}
		});

	})



function myEducation() {
	debugger;
	var data = {};
	data.Name = $("#name").val();
	data.Grade = $("#grade").val();
	data.Degree = $("#degree").val();
	data.FieldOfStudy = $("#fieldofstudy").val();
	data.StartDate = $("#startdate").val();
	data.EndDate = $("#enddate").val();
	if (data != "") {
		let educationQalification = data;
		$.ajax({
			type: 'Post',
			dataType: 'Json',
			url: '/user/EducationalQualification',
			data:
			{
				education:educationQalification,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = '/User/Profile';
					successAlertWithRedirect(result.msg, url);
				}
				else {
					errorAlert(result.msg);
				}
			},
			error: function (ex) {
				errorAlert("Error Occured,try again.");
			}
		})
	}
}
function mySkill(){
	debugger;
	var data = {};
	data.Name = $("#skillname").val();
	if (data != "") {
		let skillJob = data;
		$.ajax({
			type: 'Post',
			dataType: 'Json',
			url: '/user/Skill',
			data:
			{
				skill :skillJob,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = '/User/Profile';
					successAlertWithRedirect(result.msg, url);
				}
				else {
					errorAlert(result.msg);
				}
			},
			error: function (ex) {
				errorAlert("Error Occured,try again.");
			}
		})
	}
}
var profilePicture = "";

function ConvertPictureToBase64(e) {
	debugger;
	var profileInputId = e.target.id;
	var pix = document.getElementById(profileInputId).files;
	if (pix[0] != null) {
		const reader = new FileReader();
		reader.readAsDataURL(pix[0]);
		reader.onload = function () {
			profilePicture = reader.result;
		}
	}
}

function myPersonalinfo() {
	debugger;
	var data = {};
	data.FirstName = $("#firstname").val();
	data.LastName = $("#lastname").val();
	data.Email = $("#email").val();
	data.PhoneNumber = $("#Phone").val();
	data.Country = $("#country").val();
	data.State = $("#state").val();
	data.Address = $("#address").val();
	data.Gender = $("#gender").val();
	data.Birthdate = $("#birthdate").val();
	data.Description = $("#describe").val();
	data.ProfilePicture = profilePicture;
	if (data != "") {
		var editPersonalInfo = data;
		$.ajax({
			type: 'Post',
			dataType: 'Json',
			url: '/user/EditPersonalInfo',
			data:
			{
				personalInfo: editPersonalInfo,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
				var url = '/User/Profile';
				//location.href = url;
					successAlertWithRedirect(result.msg, url);
				}
				else {
					errorAlert(result.msg);
				}
			},
			error: function (ex) {
				errorAlert("Error Occured,try again.");
			}
		})
	}
}


$(document).ready(function () {
	$(".summernote").summernote({
		height: 100,
		tabsize: 2
	});
});
var Picture = "";

//function ConvertPictureToBase64(e) {
//	var profileInputId = e.target.id;
//	var picture = document.getElementById(profileInputId).files;
//	if (picture[0] != null) {
//		const reader = new FileReader();
//		reader.readAsDataURL(picture[0]);
//		reader.onload = function () {
//			picture = reader.result;
//		}
//	}
//}
function myDelistjob(id) {
	debugger;
	$.ajax({
		type: 'Post',
		dataType: 'Json',
		url: '/Admin/Delist',
		data:
		{
			id: id,
		},
		success: function (result) {
			debugger;
			if (!result.isError) {
				//ShowSwal("success", result.msg);
				successAlert(result.msg)
			}
		},
		error: function (ex) {
			errorAlert("Error Occured,try again.");
		}
			
	})
}

function redirect() {
	debugger;
	location.href = '/User/JobApplication';
}

var base64CV = "";
var base64Resume = "";

function ConvertFilesToBase64(e) {
	debugger;
	var fileInputId = e.target.id;
	var fileName = e.target.name;
	var file = document.getElementById(fileInputId).files;
	if (file[0] != null) {
		const reader = new FileReader();
		reader.readAsDataURL(file[0]);
		reader.onload = function () {
			if (fileName == "Resume") {
				base64Resume = reader.result;
			} else {
				base64CV = reader.result;
            }
		 
		}
	}
}


function GetSkillById(id) {
	debugger;
	$.ajax({
		type: 'GET',
		url: '/User/GetSkill', // we are calling json method
		dataType: 'json',
		data:
		{
			id: id
		},
		success: function (data) {
			debugger
			if (!data.isError) {

				$("#editId").val(data.data.id);
				$("#skillsname").val(data.data.name);
			}
		},
		error: function (ex) {
			"please fill the form correctly" + errorAlert(ex);
		}
	});
};

function editSkill() {
	debugger;
	var data = {};
	data.Id = $("#editId").val();
	data.Name = $("#skillsname").val();
	if (data != "")
	{
		var skilltoEdit = data;
		$.ajax({
			type: 'Post',
			dataType: 'Json',
			url: '/user/EditSkill',
			data:
			{
				skill: skilltoEdit,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = '/User/Profile';
					//location.href = url;
					successAlertWithRedirect(result.msg, url);
				}
				else {
					errorAlert(result.msg);
				}
			},
			error: function (ex) {
				errorAlert("Error Occured,try again.");
			}
		})
	}
}
function dateToInput(dateString) {

	var now = new Date(dateString);
	var day = ("0" + now.getDate()).slice(-2);
	var month = ("0" + (now.getMonth() + 1)).slice(-2);

	var today = now.getFullYear() + "-" + (month) + "-" + (day);

	return today;
}

function GetQualificationById(id) {
	debugger;
	$.ajax({
		type: 'GET',
		url: '/User/GetEducation',
		dataType: 'json',
		data:
		{
			id: id
		},
		success: function (result) {
			debugger
			if (!result.isError) {

				$("#educateId").val(result.data.id);
				$("#eduName").val(result.data.name);
				$("#eduDegree").val(result.data.degree);
				$("#eduFieldofstudy").val(result.data.fieldOfStudy);
				$("#eduGrade").val(result.data.grade);
				$("#eduStartdate").val(dateToInput(result.data.startDate));
				$("#eduEnddate").val(dateToInput(result.data.endDate));
			}
		},
		error: function (ex) {
			"please fill the form correctly" + errorAlert(ex);
		}
	});
};

function editEducation() {
	debugger;
	var data = {};
	data.Id = $("#educateId").val();
	data.Name = $("#eduName").val();
	data.Degree = $("#eduDegree").val();
	data.FieldOfStudy = $("#eduFieldofstudy").val();
	data.Grade = $("#eduGrade").val();
	data.StartDate = $("#eduStartdate").val();
	data.EndDate = $("#eduEnddate").val();
	if (data != "")
	{
		var educationtoEdit = data;
		$.ajax({
			type: 'Post',
			dataType: 'Json',
			url: '/user/EditEducation',
			data:
			{
				education: educationtoEdit,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = '/User/Profile';
					successAlertWithRedirect(result.msg, url);
				}
				else {
					errorAlert(result.msg);
				}
			},
			error: function (ex) {
				errorAlert("Error Occured,try again.");
			}
		})
	}
}


function dateToInput(dateString) {

	var now = new Date(dateString);
	var day = ("0" + now.getDate()).slice(-2);
	var month = ("0" + (now.getMonth() + 1)).slice(-2);

	var today = now.getFullYear() + "-" + (month) + "-" + (day);

	return today;
}


function GetWorkById(id) {
	debugger;
	$.ajax({
		type: 'GET',
		url: '/User/GetWork', 
		dataType: 'json',
		data:
		{
			id: id
		},
		success: function (data) {
			debugger
			if (!data.isError) {

				$("#workId").val(data.data.id);
				$("#workName").val(data.data.workPlace);
				$("#worLocation").val(data.data.location);
				$("#worDate").val(dateToInput(data.data.dateAdded));
				$("#worDateclosed").val(dateToInput(data.data.dateClosed));
				$("#worDiscription").val(data.data.discription);
			}
		},
		error: function (ex) {
			"please fill the form correctly" + errorAlert(ex);
		}
	});
};

function EditWorK() {
	debugger;
	var data = {};
	data.Id = $("#workId").val();
	data.workPlace = $("#workName").val();
	data.location = $("#worLocation").val();
	data.dateAdded = $("#worDate").val();
	data.dateClosed = $("#worDateclosed").val();
	data.discription = $("#worDiscription").val();
	if (data != "")
	{
		var worktoEdit = data;
		$.ajax({
			type: 'Post',
			dataType: 'Json',
			url: '/user/EditWork',
			data:
			{
				work: worktoEdit,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = '/User/Profile';
					successAlertWithRedirect(result.msg, url);
				}
				else {
					errorAlert(result.msg);
				}
			},
			error: function (ex) {
				errorAlert("Error Occured,try again.");
			}
		})
	}
}


function myJob(jobId) {
	debugger;
	var data = {};
	data.Resume = base64Resume;
	data.Cv = base64CV;
	data.JobId = jobId;
	if (data.Cv != "" && data.Resume != "" && data.JobId != "") {
		let jobData = JSON.stringify(data);
		$.ajax({
			type: 'Post',
			dataType: 'Json',
			url: '/User/JobApplication',
			data:
			{
				jobApplication: jobData,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = '/User/Jobs';
					successAlertWithRedirect(result.msg, url);
				}
			},
			error: function (ex) {
				errorAlert("Error Occured,try again.");
			}

		})
	} else {
		errorAlert("Error Occured,try again.");
	}
}
function myfilter() {
		debugger;
		var data = {};
		data.CompanyName = $("#companyname").val();
		data.Location = $("#location").val();
		data.Jobtitle = $("#jobtitle").val();
	if (data.CompanyName != "" || data.Location != "" || data.Jobtitle != "") {
		var filter = JSON.stringify(data);
			$.ajax({
				type: 'Post',
				dataType: 'Json',
				url: '/User/JobSearch',
				data:
				{
					searchData: filter,
				},
				success: function (result) {
					debugger;
					if (!result.isError) {
						$("#addJob").empty();
						$.each(result.data, function (i, job) {
							$("#addJob").append(
								' <div class="card border hover p-4 mt-2 border-info mb-0 text-black" id="' + job.id + '">' +
								'<div class="row" ><div class="col-8  bbb-text "><div class=" row list-inline-item">' +
								'<h3 class="fs-14 mb-1 ml-3 " style="font-size:16px">' + job.title + '</h3></div>' +
								'<div class=" row list-inline-item"><h5 class=" fs-14 mb-1 text-primary ml-3">' + job.companyName + '</h5></div>' +
								'<div class=" row list-inline-item"><p class=" fs-14 mb-1 ml-3"><i class="fa fa-map-marker"></i>' + job.location + '</p> </div>' +
								'<div class=" row list-inline-item "><p class=" fs-14 mb-1  ml-3"> $' + job.salary + '</div>' +
								'<div class=" row ml-2"><span class=" text-primary offset-12 p-1">' + job.type + '</span> </div></div>' +
								'<div class="col-md-3"><a class="d-flex justify-content-end" style="color:#FFFF33;height:20px; margin-left:10px;">' +
								'<img src="/image/avatar1.jpg" height="100" width="100" class="rounded-circle" alt="Cinque Terre"/></a></div><div>' +
								'<a class="btn btn-primary float-left offset-11" asp-controller="User" asp-action="JobApplication" asp-route-id="'+ job.id +'">Apply</a></div></div></div>'
							);
                        });
					}
					else {
						errorAlert(result.msg);
					}

				},
				error: function (ex) {
					errorAlert("Error Occured,try again.");
				}

			})
		} else {
			errorAlert("Fill the input ti filter.");
		}

    }
