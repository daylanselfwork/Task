var vue = new Vue({
    el:"#tobody_data",
    data:{
        tasks:null
    },
    methods:{
          //删除任务
          deleteTask:function(id){
              var results = confirm("确定删除ID为："+id+"的任务吗？");
              if(results){                  
                  $.ajax({
                      url:"https://localhost:44308/api/Data",
                      type:"DELETE",                   
                      data:{
                          ID:id
                      },
                      success:function(data){
                          alert(data);
                      },
                      error:function(){
                        alert("删除失败");
                      }
                  });
              }
              else {
                  alert("取消了删除");
                  
              }
             
          }
    }
      
    
});

$.ajax({
    url:"https://localhost:44308/api/Data",
    method:"GET",
    success:function(data){           
        vue.tasks = data;
        console.log(vue.tasks);
    },
    error:function(){
        console.log("获取数据失败");
    }        
});
